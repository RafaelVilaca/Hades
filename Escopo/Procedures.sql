Use SMN_Hades

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
						@Senha, 
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
		UPDATE Usuario 
			SET Nome = @Nome, 
				Email = @Email, 
				Senha = @Senha, 
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
												   '27/10/2017'

	*/

	@Nome VARCHAR(20),
	@QtdItens INT,
	@DataSorteio DATETIME

	AS 
	BEGIN
		INSERT INTO Sorteio (
								Nome,
								QtdItens,
								DataSorteio,
								DataCadastro,
								Ativo
							)
			VALUES(
					@Nome,
					@QtdItens,
					@DataSorteio,
					GETDATE(),
					1
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
	Ex................: EXEC [dbo].[SP_UpdSorteio]  'Sorteando Caderninhos',
													1,
													'10/10/2017'

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
				ON u.Id = e.Id
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
					ON u.Id = e.Id
			WHERE e.Id = @Id
		
		SELECT v.TipoVoto AS Voto, 
			   v.Justificativa AS Justificativa, 
			   u.Nome AS NomeUsuario 
			FROM Votacao v
				INNER JOIN Usuario u 
					ON u.Id = v.IdUsuario
			WHERE v.IdEnquete = @Id
			ORDER BY TipoVoto;
		
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
		SELECT * FROM Usuario WHERE Id = @Id
	END
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[SP_ListarSorteio]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[SP_ListarSorteio]
GO

CREATE PROCEDURE [dbo].[SP_ListarSorteio]

	/*
	Documenta��o
	Arquivo Fonte.....: Procedures.sql
	Objetivo..........: Listar Sorteios
	Autor.............: Rafael Vila�a
 	Data..............: 09/10/2017
	Ex................: EXEC [dbo].[SP_ListarSorteio]

	*/

	AS 
	BEGIN
		SELECT s.Nome,
			   s.QtdItens,
			   s.DataSorteio, 
			   s.DataCadastro, 
			   (
					SELECT COUNT(*) 
						FROM SorteioParticipante sp 
						WHERE sp.IdSorteio = s.Id
			   ) AS NumeroParticipantes
			FROM Sorteio s
			WHERE s.Ativo = 1
			GROUP BY s.Id, 
					 s.Nome,
					 s.DataSorteio,
					 s.QtdItens,
					 s.DataCadastro
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
		SELECT s.Nome,
			   s.QtdItens,
			   s.DataSorteio, 
			   s.DataCadastro, 
			   (
					SELECT COUNT(*) 
						FROM SorteioParticipante sp 
						WHERE sp.IdSorteio = Id
				) AS NumeroParticipantes
		FROM Sorteio s
		WHERE s.Id = @Id
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
		SELECT sp.IdUsuario AS IdUsua, 
			   u.Nome AS NomeUsuario 
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
 	Data..............: 01/01/2015
	Ex................: EXEC [dbo].[SP_DeletarParticipantesSorteio]

	*/

	@Id int

	AS 
	BEGIN
		DELETE FROM SorteioParticipante 
			   WHERE IdSorteio = @Id
	END
GO
