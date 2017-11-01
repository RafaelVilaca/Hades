Use SMN_Hades

GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_AddVoto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_AddVoto]
GO

CREATE PROCEDURE [dbo].[SP_AddVoto]

	/*
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Inserir as vota��es que forem feitas
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddVoto] 1,
												1,
												'Testando inser��es',
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Inserir as vota��es que forem feitas
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar vota��o j� realizada
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdateVoto] 1,
												   1,
												   'Testando inser��es',
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona Usuarios
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona Enquetes para vota��es
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_AddEnquete] 'Cachorro'
												   'Creio que um cachorro far� bem para o ambiente'
												   1,
												   150.00,
												   'C�o que mia'
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar Usuario
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualizar Enquete
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_UpdEnquete] 1,
												   'Cachorro'
												   'Creio que um cachorro far� bem para o ambiente'
												   1,
												   150.00,
												   'C�o que mia'
	*/

	@IdEnq INT,
	@Titulo VARCHAR(20),
	@Descricao VARCHAR(150),
	@Ativo BIT,
	@Valor DECIMAL(10,2),
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
	Documenta��o
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Adiciona participantes de sorteios
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Atualiza o Sorteio
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar todas as enquetes ativas
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: ArquivoFonte.sql
	Objetivo..........: Listar enquete individualmente
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarEnquetePorId] 1

	*/

	@Id INT

	AS 
	BEGIN
		SELECT e.Titulo,
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar todos usu�rios
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarUsuarios]

	*/

	AS 
	BEGIN
		SELECT * 
			FROM Usuario 
			ORDER BY Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarUsuarioPorNome]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarUsuarioPorNome]
GO

CREATE PROCEDURE [dbo].[SP_ListarUsuarioPorNome]
	@Nome varchar(100)
	/*
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Lista usu�rio por nome
	Autor.............: Rafael Vila�a
 	Data..............: 17/10/2017
	Ex................: EXEC [dbo].[SP_ListarUsuarioPorNome] 'Rafael Vila�a'

	*/

	AS 
	BEGIN
		SELECT TOP 1 * 
			FROM Usuario 
			WHERE Nome = @Nome
			ORDER BY Nome
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarUsuarioPorId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarUsuarioPorId]
GO

CREATE PROCEDURE [dbo].[SP_ListarUsuarioPorId]

	/*
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Usuario 
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Sorteios
	Autor.............: Rafael Vila�a
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
			   s.FoiSorteado
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
					 s.FoiSorteado
	
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarSorteioPorId]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarSorteioPorId]
GO

CREATE PROCEDURE [dbo].[SP_ListarSorteioPorId]

	/*
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Sorteios por ID
	Autor.............: Rafael Vila�a
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
	Documenta��o
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Pega informa��es de Participantes dos sorteios
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Alterar o Status da Enquete
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Altera o status do usuario
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Deleta participante do Sorteio
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Seta participante do Sorteio como Vencedor
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Seta participante do Sorteio para um novo sorteio
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Formata senhas para verifica��es
	Autor.............: Rafael Vila�a
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
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Lista vencedores do sorteio
	Autor.............: Rafael Vila�a
 	Data..............: 01/11/2017
	Ex................: EXEC [dbo].[SP_GetVencedores] 1

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
			INNER JOIN Sorteio s
				ON s.Id = sp.IdSorteio
					AND s.Id = @idSorteio
	END
GO
