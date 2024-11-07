-- Créer la table File
CREATE TABLE IF NOT EXISTS File (
    FileId VARCHAR(20) PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    path VARCHAR(255) NOT NULL,
    size INT NOT NULL,
    date TIMESTAMP NOT NULL,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS Secret (
    secret VARCHAR(20) PRIMARY KEY,
    MaxSize INT NOT NULL,
    MaxUpload INT NOT NULL
);

-- Créer la table Upload
CREATE TABLE IF NOT EXISTS Upload (
    username VARCHAR(50) NOT NULL,
    fileId VARCHAR(20) REFERENCES File(FileId) ON DELETE CASCADE,
    date TIMESTAMP NOT NULL,
    Maxd INT,
    secret VARCHAR(20) REFERENCES Secret(secret),
    PRIMARY KEY (username, fileId)  -- Clé primaire composée
);

-- Créer la table Download
CREATE TABLE IF NOT EXISTS Download (
    username VARCHAR(50) NOT NULL,
    fileId VARCHAR(20) REFERENCES File(FileId) ON DELETE CASCADE,
    date TIMESTAMP NOT NULL,
    PRIMARY KEY (username, fileId, date)  -- Clé primaire composée incluant la date pour éviter les doublons
);

-- Créer la table Expiration
CREATE TABLE IF NOT EXISTS Expiration (
    fileId VARCHAR(20) UNIQUE REFERENCES File(FileId) ON DELETE CASCADE,
    date TIMESTAMP NOT NULL,
    count INT,
    PRIMARY KEY (fileId)  -- Clé primaire sur fileId, car chaque fichier a une seule date d'expiration
);

