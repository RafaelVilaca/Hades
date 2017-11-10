CREATE Database SMN_Hades
GO

Use SMN_Hades
GO



CREATE TABLE Usuario (
	Id INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	DataCadastro DATETIME NOT NULL,
	Senha VARCHAR(50) NOT NULL,
	Administrador BIT NOT NULL,
	Ativo BIT NOT NULL )
GO


INSERT INTO Usuario 
	VALUES ('Administrador',
			'adm@smn.com.br',
			GETDATE(),
			(SELECT CONVERT(VARCHAR(32), HashBytes('MD5', '123123'), 2)), 
			1, 
			1)
GO


CREATE TABLE Enquete (
	Id INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(20) NOT NULL,
	Descricao VARCHAR(150) NOT NULL,
	IdUsuario INT NOT NULL,
	DataEnquete DATETIME NOT NULL,
	Ativo BIT NOT NULL,
	Valor DECIMAL(10, 2),
	LocalCotado VARCHAR(50),
	CONSTRAINT FK_Usuario_Enquete FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id) )
GO


CREATE TABLE Votacao (
	IdEnquete INT NOT NULL,
	IdUsuario INT NOT NULL,
	Justificativa VARCHAR(200),
	TipoVoto BIT,
	CONSTRAINT FK_Usuario_Votacao FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id),
	CONSTRAINT FK_Enquete_Votacao FOREIGN KEY (IdEnquete) REFERENCES Enquete(Id),
	CONSTRAINT PK_UsuaEnqPK PRIMARY KEY (IdUsuario, IdEnquete) )
GO


CREATE TABLE Sorteio(
	Id INT IDENTITY PRIMARY KEY,
	Nome VARCHAR(50),
	QtdItens INT,
	DataSorteio DATETIME,
	DataCadastro DATETIME,
	Ativo BIT, 
	IdCriador INT,
	FoiSorteado BIT DEFAULT 0,
	CONSTRAINT FK_Usuario_Sorteio FOREIGN KEY (IdCriador) REFERENCES Usuario(Id) )
GO


CREATE TABLE SorteioParticipante(
	IdUsuario INT NOT NULL,
	IdSorteio INT NOT NULL,
	DataCadastro DATETIME,
	IndSorteado BIT DEFAULT 0,
	CONSTRAINT FK_Usuario_SorteioPArticipante FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id),
	CONSTRAINT FK_Participante_Sorteio FOREIGN KEY (IdSorteio) REFERENCES Sorteio(Id),
	CONSTRAINT PK_UsuaSortPK PRIMARY KEY (IdUsuario, IdSorteio) )
GO


CREATE TABLE Campanha(
	IdCampanha INT IDENTITY PRIMARY KEY,
	DescCampanha VARCHAR(100),
	DataCadastro DATETIME,
	DataLimite DATETIME,
	ValorCampanha DECIMAL(10,2),
	IndAtivo BIT,
	IdCriador INT,
	CONSTRAINT FK_Usuario_CampanhaCriador FOREIGN KEY (IdCriador) REFERENCES Usuario(Id)
)
GO



CREATE TABLE CampanhaParticipante(
	IdUsuario INT NOT NULL,
	IdCampanha INT NOT NULL,
	DataCadastro DATETIME,
	CONSTRAINT FK_Usuario_CampanhaParticipante FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id),
	CONSTRAINT FK_Participante_Campanha FOREIGN KEY (IdCampanha) REFERENCES Campanha(IdCampanha),
	CONSTRAINT PK_UsuaCampPK PRIMARY KEY (IdUsuario, IdCampanha) 
)
GO
