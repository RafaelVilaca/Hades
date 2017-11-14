Use SMN_Hades

GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddVoto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddVoto]
GO

CREATE PROCEDURE [dbo].[SP_AddVoto]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Inserir as votações que forem feitas
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddVoto] 1,
												1,
												'Testando inserções',
												1
	*/

	@UsuarioId INT,
	@EnqueteId INT,
	@Justificativa VARCHAR(200),
	@TipoVoto BIT

	AS 
	BEGIN
		INSERT INTO [dbo].[Votacao] ( 
									  IdEnquete,
									  IdUsuario,
									  Justificativa,
									  TipoVoto
									 )
			VALUES (
					 @EnqueteId, 
					 @UsuarioId, 
					 @Justificativa, 
					 @TipoVoto
				   )
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_GetVotos]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_GetVotos]
GO

CREATE PROCEDURE [dbo].[SP_GetVotos]
	@UsuarioId INT = null,
	@EnqueteId INT
	AS 
	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Inserir as votações que forem feitas
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_GetVotos] 1, 6
	*/
	
	BEGIN
		SELECT  e.Titulo,
				u.Nome AS NomeUsuario,
			    v.Justificativa,
				v.TipoVoto
			FROM [dbo].[Votacao] v
				INNER JOIN [dbo].[Enquete] e WITH(NOLOCK)
					ON e.Id = v.IdEnquete
				INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
					ON u.Id = v.IdUsuario
			WHERE (@UsuarioId IS NULL OR v.IdUsuario = @UsuarioId)
				AND (@EnqueteId IS NULL OR v.IdEnquete = @EnqueteId)
			ORDER BY u.Nome
					
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdVoto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdVoto]
GO

CREATE PROCEDURE [dbo].[SP_UpdVoto]


	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar votação já realizada
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdateVoto] 1,
												   1,
												   'Testando inserções',
												   1
	*/

	@IdUsua INT,
	@IdEnq INT,
	@Justificativa VARCHAR(200),
	@TipoVoto BIT

	AS 
	BEGIN
		UPDATE Votacao 
			SET Justificativa = @Justificativa, 
				TipoVoto = @TipoVoto
			WHERE IdEnquete = @IdEnq
				AND IdUsuario =  @IdUsua
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddUsuario]
GO

CREATE PROCEDURE [dbo].[SP_AddUsuario]


	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona Usuarios
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddUsuario] 'Rafael',
												   'rafael@smn.com.br',
												   '1258'

	*/

	@Nome VARCHAR(100),
	@Email VARCHAR(100),
	@Senha VARCHAR(20)

	AS 
	BEGIN

		DECLARE @SenhaConvertida VARCHAR(50) = (SELECT CONVERT(VARCHAR(32), HashBytes('MD5', @Senha), 2));

		INSERT INTO Usuario (
								Nome,
								Email,
								DataCadastro, 
								Senha,
								Administrador,
								Ativo		
							)
			VALUES (
						@Nome, 
						@Email, 
						GETDATE(), 
						@SenhaConvertida, 
						0, 
						1
					)
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddEnquete]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddEnquete]
GO

CREATE PROCEDURE [dbo].[sp_AddEnquete]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona Enquetes para votações
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddEnquete] 'Cachorro'
												   'Creio que um cachorro fará bem para o ambiente'
												   1,
												   150.00,
												   'Cão que mia'
	*/

	@Titulo VARCHAR(20),
	@Descricao VARCHAR(150),
	@UsuarioId INT,
	@Valor DECIMAL(10,2),
	@Nom_LocalCotado VARCHAR(50)

	AS 
	BEGIN
		INSERT INTO Enquete (
								Titulo,
								Descricao,
								IdUsuario,
								DataEnquete,
								Ativo,
								Valor,
								LocalCotado
							)
			VALUES (
						@Titulo, 
						@Descricao, 
						@UsuarioId, 
						GETDATE(), 
						1,
						@Valor,
						@Nom_LocalCotado
					)
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdUsuario]
GO

CREATE PROCEDURE [dbo].[SP_UpdUsuario]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar Usuario
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdUsuario] 1,
												   'Adm',
												   'adm@smn.com.br',
												   'adm123',
												   1,
												   1

	*/

	@IdUsua int,
	@Nome varchar(100),
	@Email varchar(100),
	@Senha varchar(20),
	@Administrador BIT,
	@Ativo BIT

	AS
	BEGIN

		DECLARE @SenhaConvertida VARCHAR(50) = (SELECT CONVERT(VARCHAR(32), HashBytes('MD5', @Senha), 2));
	
		UPDATE Usuario 
			SET Nome = @Nome, 
				Email = @Email, 
				Senha = @SenhaConvertida, 
				Administrador = @Administrador, 
				Ativo = @Ativo
			WHERE Id = @IdUsua
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdEnquete]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdEnquete]
GO

CREATE PROCEDURE [dbo].[SP_UpdEnquete]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar Enquete
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdEnquete] 1,
												   'Cachorro'
												   'Creio que um cachorro fará bem para o ambiente'
												   1,
												   150.00,
												   'Cão que mia'
	*/

	@IdEnq INT,
	@Titulo VARCHAR(20),
	@Descricao VARCHAR(150),
	@Ativo BIT,
	@Valor DECIMAL(15,2),
	@LocalCotado VARCHAR(50)
	
	AS 
	BEGIN
		UPDATE Enquete 
			SET Titulo = @Titulo, 
				Descricao = @Descricao, 
				Ativo = @Ativo,
				Valor = @Valor,
				LocalCotado = @LocalCotado
			WHERE Id = @IdEnq
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddSorteio]
GO

CREATE PROCEDURE [dbo].[SP_AddSorteio]


	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Inserir Sorteio
	Autor.............: ENGSOLUTIONS - Autor
 	Data..............: 01/01/2015
	Ex................: EXEC [dbo].[SP_AddSorteio] 'Cadernos'
												   2,
												   '27/10/2017',
												   1

	*/

	@Nome VARCHAR(20),
	@QtdItens INT,
	@DataSorteio DATETIME,
	@IdCriador int

	AS 
	BEGIN
		INSERT INTO Sorteio (
								Nome,
								QtdItens,
								DataSorteio,
								DataCadastro,
								Ativo,
								IdCriador,
								FoiSorteado
							)
			VALUES(
					@Nome,
					@QtdItens,
					@DataSorteio,
					GETDATE(),
					1,
					@IdCriador,
					0
				  )
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddParticipante]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddParticipante]
GO

CREATE PROCEDURE [dbo].[SP_AddParticipante]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona participantes de sorteios
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddParticipante] 1,
														1

	*/

	@IdUsuario int,
	@IdSorteio int

	AS 
	BEGIN
		INSERT INTO SorteioParticipante (
											IdUsuario,
											IdSorteio,
											DataCadastro,
											IndSorteado 											
										)
			VALUES(
					@IdUsuario, 
					@IdSorteio,
					GETDATE(),
					0
				  )

				  SELECT * FROM SorteioParticipante
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdSorteio]
GO

CREATE PROCEDURE [dbo].[SP_UpdSorteio]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualiza o Sorteio
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdSorteio]  1,
													'Sorteando Caderninhos',
													1,
													'10/10/2017',
													1

	*/

	@Id INT,
	@Nome VARCHAR(20),
	@QtdItens INT,
	@DataSorteio DATETIME,
	@Ativo BIT

	AS
	BEGIN
		UPDATE Sorteio 
			SET Nome = @Nome,
				QtdItens = @QtdItens,
				DataSorteio = @DataSorteio,
				Ativo = @Ativo
			WHERE Id = @Id
	END 
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarEnquetes]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarEnquetes]
GO

CREATE PROCEDURE [dbo].[SP_ListarEnquetes]


	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar todas as enquetes ativas
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarEnquetes]

	*/

	AS 
	BEGIN

		DECLARE @VerificaDatas DateTime = GETDATE()

		UPDATE Enquete
			SET Ativo = 0
			WHERE Id IN (SELECT e.Id
							FROM  Enquete e
							WHERE DATEDIFF(MONTH, e.DataEnquete, @VerificaDatas) >= 2)


		SELECT e.Id,
			   e.Titulo,
			   e.Descricao,
			   e.IdUsuario,
			   e.DataEnquete,
			   e.Ativo,	
			   e.Valor,
			   e.LocalCotado,
			   u.Nome AS Criador,
			   SUM(CASE WHEN v.TipoVoto = 1 THEN 1 ELSE 0 END) AS VotoFavor,
			   SUM(CASE WHEN v.TipoVoto = 0 THEN 1 ELSE 0 END) AS VotoContra
		FROM Enquete e
			INNER JOIN Usuario u 
				ON u.Id = e.IdUsuario
			LEFT JOIN Votacao v 
				ON e.Id = v.IdEnquete
		WHERE e.Ativo = 1
		GROUP BY e.Id, 
				 e.Titulo, 
				 e.Descricao, 
				 e.IdUsuario, 
				 e.DataEnquete, 
				 e.Ativo,	
				 e.Valor,
			     e.LocalCotado,
				 u.Nome
		ORDER BY e.DataEnquete DESC
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarEnquetePorId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarEnquetePorId]
GO

CREATE PROCEDURE [dbo].[SP_ListarEnquetePorId]

	/*
	Documentação
	Arquivo Fonte.....: ArquivoFonte.sql
	Objetivo..........: Listar enquete individualmente
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarEnquetePorId] 1

	*/

	@Id INT

	AS 
	BEGIN
		SELECT e.Id,
			   e.Titulo,
			   e.Descricao,
			   e.IdUsuario,
			   e.DataEnquete,
			   e.Ativo,
			   e.Valor,
			   e.LocalCotado, 
			   u.Nome AS Criador
			FROM Enquete e
				INNER JOIN Usuario u 
					ON u.Id = e.IdUsuario
			WHERE e.Id = @Id
		
		SELECT TOP 10
			   v.TipoVoto AS Voto, 
			   v.Justificativa AS Justificativa, 
			   u.Nome AS NomeUsuario 
			FROM Votacao v
				INNER JOIN Usuario u 
					ON u.Id = v.IdUsuario
			WHERE v.IdEnquete = @Id
			ORDER BY v.TipoVoto;
		
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarUsuarios]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarUsuarios]
GO

CREATE PROCEDURE [dbo].[SP_ListarUsuarios]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar todos usuários
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarUsuarios]

	*/

	AS 
	BEGIN
		SELECT * 
			FROM Usuario 			
			WHERE Nome NOT LIKE 'Administrador'
			ORDER BY Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarUsuarioPorNome]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarUsuarioPorNome]
GO

CREATE PROCEDURE [dbo].[SP_ListarUsuarioPorNome]
	@Nome varchar(100)
	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Lista usuário por nome
	Autor.............: Rafael Vilaça
 	Data..............: 17/10/2017
	Ex................: EXEC [dbo].[SP_ListarUsuarioPorNome] 'Rafael Vilaça'

	*/

	AS 
	BEGIN
		SELECT TOP 1 *
			FROM Usuario 
			WHERE (Nome = @Nome OR Email = @Nome)
			ORDER BY Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarUsuarioPorId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarUsuarioPorId]
GO

CREATE PROCEDURE [dbo].[SP_ListarUsuarioPorId]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Usuario 
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarUsuarioPorId] 1

	*/

	@Id INT

	AS 
	BEGIN
		SELECT u.*
			FROM Usuario u
			WHERE Id = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarSorteio]
GO

CREATE PROCEDURE [dbo].[SP_ListarSorteio]
	
	@CodUsua int = null
	
	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Sorteios
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarSorteio] 1

	*/

	AS 
	BEGIN

		DECLARE @VerificaDatas DateTime = GETDATE();

		UPDATE Sorteio
			SET Ativo = 0
			WHERE Id IN (SELECT s.Id
							FROM  Sorteio s
							WHERE DATEDIFF(DAY, s.DataSorteio, @VerificaDatas) > 1)

		SELECT u.Id as usua,
			   s.Id,
			   s.Nome,
			   s.QtdItens,
			   s.DataSorteio, 
			   s.DataCadastro, 
			   (
					SELECT COUNT(*) 
						FROM SorteioParticipante sp 
						WHERE sp.IdSorteio = s.Id
			   ) AS NumeroParticipantes,
			   s.IdCriador,
			   u.Nome AS NomeCriador,
			   (CASE 
					WHEN x.IdSorteio IS NULL THEN 'N'
					ELSE 'S' END) IndParticipa,
			   s.FoiSorteado,
			   s.Ativo
			FROM Sorteio s
				INNER JOIN Usuario u
					ON u.Id = s.IdCriador
				OUTER APPLY(
					SELECT TOP 1 sp.IdSorteio
						FROM SorteioParticipante sp
						WHERE sp.IdUsuario = @CodUsua	
							AND sp.IdSorteio = s.Id
				)x
			WHERE s.Ativo = 1
			GROUP BY s.Id, 
					 s.Nome,
					 s.DataSorteio,
					 s.QtdItens,
					 s.DataCadastro,
					 s.IdCriador,
					 u.Nome,
					 u.Id,
					 x.IdSorteio,
					 s.FoiSorteado,
					 s.Ativo
	
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarSorteioPorId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarSorteioPorId]
GO

CREATE PROCEDURE [dbo].[SP_ListarSorteioPorId]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Sorteios por ID
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[sp_ListarSorteioPorId] 1

	*/

	@Id int

	AS 
	BEGIN
		SELECT s.Id,
			   s.Nome,
			   s.QtdItens,
			   s.DataSorteio, 
			   s.DataCadastro, 
			   (
					SELECT COUNT(*) 
						FROM SorteioParticipante sp 
						WHERE sp.IdSorteio = s.Id
			   ) AS NumeroParticipantes,
			   s.IdCriador,
			   u.Nome AS NomeCriador,
			   s.Ativo
			FROM Sorteio s
				INNER JOIN Usuario u
					ON u.Id = s.IdCriador
		WHERE s.Id = @Id
			AND s.Ativo = 1

		SELECT  u.Id AS Id_Participante,
				u.Nome AS Nome_Participante
			FROM Usuario u
				INNER JOIN SorteioParticipante sp
					ON sp.IdUsuario = u.Id
			WHERE sp.IdSorteio = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_DelSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_DelSorteio]
GO

CREATE PROCEDURE [dbo].[SP_DelSorteio]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Desativa um Sorteio
	Autor.............: ENGSOLUTIONS - Autor
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_DelSorteio] 1

	*/

	@Id int

	AS 
	BEGIN
		UPDATE Sorteio 
			SET Ativo = 0
			WHERE Id = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_GetParticipantes]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_GetParticipantes]
GO

CREATE PROCEDURE [dbo].[SP_GetParticipantes]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Pega informações de Participantes dos sorteios
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_GetParticipantes] 1

	*/

	@Id int

	AS 
	BEGIN
		SELECT sp.IdUsuario AS Id_Participante, 
			   u.Nome AS Nome_Participante 
			FROM SorteioParticipante sp
				INNER JOIN Sorteio s 
					ON s.Id = sp.IdSorteio
				INNER JOIN Usuario u 
					ON u.Id = sp.IdUsuario
			WHERE s.Id = @Id
			GROUP BY s.Id, 
					 sp.IdUsuario, 
					 sp.IdSorteio, 
					 s.Nome,
					 u.Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AlteraStatusEnquete]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AlteraStatusEnquete]
GO

CREATE PROCEDURE [dbo].[SP_AlteraStatusEnquete]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Alterar o Status da Enquete
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AlteraStatusEnquete] 1,
															0

	*/

	@Id int,
	@Ativo bit
	
	AS 
	BEGIN
		UPDATE Enquete 
			SET Ativo = @Ativo 
			WHERE Id = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AlteraStatusUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AlteraStatusUsuario]
GO

CREATE PROCEDURE [dbo].[SP_AlteraStatusUsuario]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Altera o status do usuario
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AlteraStatusUsuario] 1,
															0
	*/

	@Id int,
	@Ativo bit
	
	AS 
	BEGIN
		UPDATE Usuario 
			SET Ativo = @Ativo 
			WHERE Id = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_DeletarParticipantesSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_DeletarParticipantesSorteio]
GO

CREATE PROCEDURE [dbo].[SP_DeletarParticipantesSorteio]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Deleta participante do Sorteio
	Autor.............: Rafael Vilaça
 	Data..............: 25/10/2017
	Ex................: EXEC [dbo].[SP_DeletarParticipantesSorteio]

	*/

	@IdSorteio INT,
	@IdUsuario INT

	AS 
	BEGIN
		DELETE FROM SorteioParticipante 
			   WHERE IdSorteio = @IdSorteio			   
				AND IdUsuario = @IdUsuario
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdVencedoresSorteios]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdVencedoresSorteios]
GO

CREATE PROCEDURE [dbo].[SP_UpdVencedoresSorteios]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Seta participante do Sorteio como Vencedor
	Autor.............: Rafael Vilaça
 	Data..............: 25/10/2017
	Ex................: EXEC [dbo].[SP_UpdVencedoresSorteios] 1, 1

	*/

	@IdSorteio INT,
	@IdUsuario INT

	AS 
	BEGIN
		UPDATE Sorteio
			SET FoiSorteado = 1
			WHERE Id = @IdSorteio

		UPDATE SorteioParticipante
			SET IndSorteado = 1 
			WHERE IdSorteio = @IdSorteio
				AND IdUsuario = @IdUsuario
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdVencedoresSorteiosNovamente]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdVencedoresSorteiosNovamente]
GO

CREATE PROCEDURE [dbo].[SP_UpdVencedoresSorteiosNovamente]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Seta participante do Sorteio para um novo sorteio
	Autor.............: Rafael Vilaça
 	Data..............: 25/10/2017
	Ex................: EXEC [dbo].[SP_UpdVencedoresSorteiosNovamente] 1

	*/

	@IdSorteio INT

	AS 
	BEGIN
		UPDATE SorteioParticipante
			SET IndSorteado = 0
			WHERE IdSorteio = @IdSorteio
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_FormatandoSenha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_FormatandoSenha]
GO

CREATE PROCEDURE [dbo].[SP_FormatandoSenha]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Formata senhas para verificações
	Autor.............: Rafael Vilaça
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_FormatandoSenha] 'rafael'

	*/

	@Senha VARCHAR(20)

	AS 
	BEGIN
		SELECT CONVERT(VARCHAR(32), HashBytes('MD5', @Senha), 2) AS SenhaFormatada
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_GetVencedores]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_GetVencedores]
GO

CREATE PROCEDURE [dbo].[SP_GetVencedores]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Lista vencedores do sorteio
	Autor.............: Rafael Vilaça
 	Data..............: 01/11/2017
	Ex................: EXEC [dbo].[SP_GetVencedores] 3
	*/

	@idSorteio INT

	AS 
	BEGIN
		SELECT u.Nome AS Nom_Vencedor,
			   s.Id as IdSorteio,
			   s.Nome as Nom_Sorteio
			FROM Usuario u
			INNER JOIN SorteioParticipante sp
				ON sp.IdUsuario = u.Id
					AND sp.IndSorteado = 1
			RIGHT JOIN Sorteio s
				ON s.Id = sp.IdSorteio
					AND s.Id = @idSorteio
			WHERE s.Id = @idSorteio
			ORDER BY u.Nome
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarCampanhas]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarCampanhas]
GO

CREATE PROCEDURE [dbo].[SP_ListarCampanhas]

	@CodUsua int = null

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Lista todas as campanhas existentes ativas
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_ListarCampanhas] 1
	*/
	
	AS 	

	BEGIN
		
		DECLARE @VerificaDatas DateTime = GETDATE()

		UPDATE [dbo].[Campanha]
			SET IndAtivo = 0
			WHERE IdCampanha IN (SELECT c.IdCampanha
									FROM  Campanha c
									WHERE DATEDIFF(Day, c.DataLimite, @VerificaDatas) >= 1)

		SELECT c.DescCampanha,
			   c.IdCampanha,
			   c.DataCadastro,
			   c.DataLimite,
			   c.ValorCampanha,
			   c.IndAtivo,
			   c.IdCriador,
			   u.Nome AS NomeUsuario,
			   (
					SELECT COUNT(*) 
						FROM CampanhaParticipante cp 
						WHERE cp.IdCampanha = c.IdCampanha
			   ) AS NumeroParticipantes,
			   (CASE 
					WHEN x.IdCampanha IS NULL THEN 'N'
					ELSE 'S' END) IndParticipante
			FROM [dbo].[Campanha] c WITH(NOLOCK)
			INNER JOIN Usuario u
				ON c.IdCriador = u.Id	
			OUTER APPLY(
				SELECT TOP 1 cp.IdCampanha
					FROM CampanhaParticipante cp
					WHERE cp.IdUsuario = @CodUsua	
						AND cp.IdCampanha = c.IdCampanha
			)x							
			WHERE c.IndAtivo = 1
			ORDER BY c.DescCampanha
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarCampanha]
GO

CREATE PROCEDURE [dbo].[SP_ListarCampanha]

	@idCampanha INT

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Lista todas as campanha existente ativa
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_ListarCampanha] 2
	*/
	
	AS 

	BEGIN
		SELECT c.DescCampanha,
			   c.IdCampanha,
			   c.DataCadastro,
			   c.DataLimite,
			   c.ValorCampanha,
			   c.IndAtivo,
			   c.IdCriador,
			   u.Nome AS NomeUsuario
			FROM [dbo].[Campanha] c WITH(NOLOCK)
			INNER JOIN Usuario u
				ON c.IdCriador = u.Id								
			WHERE c.IdCampanha = @idCampanha

		SELECT u.Nome AS NomeUsuario,
			   c.IdCampanha,
			   u.Id AS IdUsuario
			FROM Usuario u
			INNER JOIN CampanhaParticipante cp
				ON cp.IdUsuario = u.Id
			INNER JOIN Campanha c
				ON c.IdCampanha = cp.IdCampanha
			WHERE c.IdCampanha = @idCampanha
			ORDER BY u.Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_InsCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_InsCampanha]
GO

CREATE PROCEDURE [dbo].[SP_InsCampanha]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Insere a campanha
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_InsCampanha] 'Açucar para ONG', '01/12/2018', '11,82', 1
	*/

	@DescCampanha VARCHAR(100), 
	@DataLimite DATETIME,
	@ValorCampanha decimal(10,2),
	@IdCriador INT

	AS 
	BEGIN
		INSERT INTO [dbo].[Campanha] ( DescCampanha, DataCadastro, DataLimite, ValorCampanha, IndAtivo, IdCriador )
							   VALUES( @DescCampanha, GETDATE(), @DataLimite, @ValorCampanha, 1, @IdCriador )
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_UpdCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_UpdCampanha]
GO

CREATE PROCEDURE [dbo].[SP_UpdCampanha]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Altera a campanha
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_UpdCampanha] 1 , 'Açucar para ONG', '01/12/2018', '11,82', 1
	*/

	@IdCampanha INT,
	@DescCampanha VARCHAR(100), 
	@DataLimite DATETIME,
	@ValorCampanha decimal(10,2),
	@IndAtivo BIT

	AS 
	BEGIN
		UPDATE Campanha
			SET IndAtivo = @IndAtivo,
				DescCampanha = @DescCampanha, 
				DataLimite = @DataLimite,
				ValorCampanha = @ValorCampanha
			WHERE IdCampanha = @IdCampanha
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_DelCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_DelCampanha]
GO

CREATE PROCEDURE [dbo].[SP_DelCampanha]

	/*
	Documentação
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Seta inativo a campanha
	Autor.............: Rafael Vilaça
 	Data..............: 25/10/2017
	Ex................: EXEC [dbo].[SP_DelCampanha] 1

	*/

	@IdCampanha INT

	AS 
	BEGIN
		UPDATE Campanha
			SET IndAtivo = 0
			WHERE IdCampanha = @IdCampanha
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_GetParticipantesCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_GetParticipantesCampanha]
GO

CREATE PROCEDURE [dbo].[SP_GetParticipantesCampanha]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Lista de participantes da Campanha
		Autor.............: Rafael Vilaça
 		Data..............: 01/11/2017
		Ex................: EXEC [dbo].[SP_GetParticipantesCampanha] 3
	*/

	@idCampanha INT

	AS 
	BEGIN
		SELECT u.Id AS IdUsuario,
			   u.Nome AS NomParticipante,
			   c.IdCampanha
			FROM Usuario u
			INNER JOIN CampanhaParticipante cp
				ON cp.IdUsuario = u.Id
			RIGHT JOIN Campanha c
				ON c.IdCampanha = cp.IdCampanha
			WHERE c.IdCampanha = @idCampanha
			ORDER BY u.Nome
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_InsParticipanteCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_InsParticipanteCampanha]
GO

CREATE PROCEDURE [dbo].[SP_InsParticipanteCampanha]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Insere participante na campanha
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_InsParticipanteCampanha] 1 , 2, '01/12/2018'
	*/

	@IdUsuario INT,
	@IdCampanha INT

	AS 
	BEGIN
		INSERT INTO [dbo].[CampanhaParticipante] ( IdUsuario, IdCampanha, DataCadastro )
										   VALUES( @IdUsuario, @IdCampanha, GETDATE() )
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_DelParticipanteCampanha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_DelParticipanteCampanha]
GO

CREATE PROCEDURE [dbo].[SP_DelParticipanteCampanha]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: deleta participante na campanha
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_DelParticipanteCampanha] 1 , 2
	*/

	@IdUsuario INT,
	@IdCampanha INT

	AS 
	BEGIN
		DELETE FROM [dbo].[CampanhaParticipante] 
			WHERE IdUsuario = @IdUsuario AND IdCampanha = @IdCampanha
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarTodasEnquetes]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarTodasEnquetes]
GO

CREATE PROCEDURE [dbo].[SP_ListarTodasEnquetes]

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Listar todas as enquetes
		Autor.............: Rafael Vilaça
 		Data..............: 09/10/2017
		Ex................: EXEC [dbo].[SP_ListarTodasEnquetes]
	*/

	AS 
	BEGIN

		DECLARE @VerificaDatas DateTime = GETDATE()

		UPDATE Enquete
			SET Ativo = 0
			WHERE Id IN (SELECT e.Id
							FROM  Enquete e
							WHERE DATEDIFF(MONTH, e.DataEnquete, @VerificaDatas) >= 2)


		SELECT e.Id,
			   e.Titulo,
			   e.Descricao,
			   e.IdUsuario,
			   e.DataEnquete,
			   e.Ativo,	
			   e.Valor,
			   e.LocalCotado,
			   u.Nome AS Criador,
			   SUM(CASE WHEN v.TipoVoto = 1 THEN 1 ELSE 0 END) AS VotoFavor,
			   SUM(CASE WHEN v.TipoVoto = 0 THEN 1 ELSE 0 END) AS VotoContra
		FROM Enquete e
			INNER JOIN Usuario u 
				ON u.Id = e.IdUsuario
			LEFT JOIN Votacao v 
				ON e.Id = v.IdEnquete
		GROUP BY e.Id, 
				 e.Titulo, 
				 e.Descricao, 
				 e.IdUsuario, 
				 e.DataEnquete, 
				 e.Ativo,	
				 e.Valor,
			     e.LocalCotado,
				 u.Nome
		ORDER BY e.DataEnquete DESC
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarTodosSorteios]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarTodosSorteios]
GO

CREATE PROCEDURE [dbo].[SP_ListarTodosSorteios]
	@CodUsua int = null
	
	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Listar Sorteios
		Autor.............: Rafael Vilaça
 		Data..............: 09/10/2017
		Ex................: EXEC [dbo].[SP_ListarTodosSorteios] 1
	*/

	AS 
	BEGIN

		DECLARE @VerificaDatas DateTime = GETDATE();

		UPDATE Sorteio
			SET Ativo = 0
			WHERE Id IN (SELECT s.Id
							FROM  Sorteio s
							WHERE DATEDIFF(DAY, s.DataSorteio, @VerificaDatas) > 1)

		SELECT u.Id as usua,
			   s.Id,
			   s.Nome,
			   s.QtdItens,
			   s.DataSorteio, 
			   s.DataCadastro, 
			   (
					SELECT COUNT(*) 
						FROM SorteioParticipante sp 
						WHERE sp.IdSorteio = s.Id
			   ) AS NumeroParticipantes,
			   s.IdCriador,
			   u.Nome AS NomeCriador,
			   (CASE 
					WHEN x.IdSorteio IS NULL THEN 'N'
					ELSE 'S' END) IndParticipa,
			   s.FoiSorteado,
			   s.Ativo
			FROM Sorteio s
				INNER JOIN Usuario u
					ON u.Id = s.IdCriador
				OUTER APPLY(
					SELECT TOP 1 sp.IdSorteio
						FROM SorteioParticipante sp
						WHERE sp.IdUsuario = @CodUsua	
							AND sp.IdSorteio = s.Id
				)x
			GROUP BY s.Id, 
					 s.Nome,
					 s.DataSorteio,
					 s.QtdItens,
					 s.DataCadastro,
					 s.IdCriador,
					 u.Nome,
					 u.Id,
					 x.IdSorteio,
					 s.FoiSorteado,
					 s.Ativo
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarTodasCampanhas]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarTodasCampanhas]
GO

CREATE PROCEDURE [dbo].[SP_ListarTodasCampanhas]

	@CodUsua int = null

	/*
		Documentação
		Arquivo Fonte.....: Procedures.sql
		Objetivo..........: Lista todas as campanhas existentes ativas
		Autor.............: Rafael Vilaça
 		Data..............: 10/11/2017
		Ex................: EXEC [dbo].[SP_ListarTodasCampanhas] 1
	*/
	
	AS 	

	BEGIN
		
		DECLARE @VerificaDatas DateTime = GETDATE()

		UPDATE [dbo].[Campanha]
			SET IndAtivo = 0
			WHERE IdCampanha IN (SELECT c.IdCampanha
									FROM  Campanha c
									WHERE DATEDIFF(Day, c.DataLimite, @VerificaDatas) >= 1)

		SELECT c.DescCampanha,
			   c.IdCampanha,
			   c.DataCadastro,
			   c.DataLimite,
			   c.ValorCampanha,
			   c.IndAtivo,
			   c.IdCriador,
			   u.Nome AS NomeUsuario,
			   (
					SELECT COUNT(*) 
						FROM CampanhaParticipante cp 
						WHERE cp.IdCampanha = c.IdCampanha
			   ) AS NumeroParticipantes,
			   (CASE 
					WHEN x.IdCampanha IS NULL THEN 'N'
					ELSE 'S' END) IndParticipante
			FROM [dbo].[Campanha] c WITH(NOLOCK)
			INNER JOIN Usuario u
				ON c.IdCriador = u.Id	
			OUTER APPLY(
				SELECT TOP 1 cp.IdCampanha
					FROM CampanhaParticipante cp
					WHERE cp.IdUsuario = @CodUsua	
						AND cp.IdCampanha = c.IdCampanha
			)x							
			ORDER BY c.DescCampanha
	END
GO
