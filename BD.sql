-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           10.2.12-MariaDB - mariadb.org binary distribution
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Copiando estrutura do banco de dados para gerenciatarefa
CREATE DATABASE IF NOT EXISTS `gerenciatarefa` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `gerenciatarefa`;

-- Copiando estrutura para tabela gerenciatarefa.tbl_empresas
CREATE TABLE IF NOT EXISTS `tbl_empresas` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Nome` tinytext NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Tabela onde fica os dados sobre as empresas\r\n';

-- Copiando dados para a tabela gerenciatarefa.tbl_empresas: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `tbl_empresas` DISABLE KEYS */;
INSERT INTO `tbl_empresas` (`ID`, `Nome`) VALUES
	(1, 'CFTVA');
/*!40000 ALTER TABLE `tbl_empresas` ENABLE KEYS */;

-- Copiando estrutura para tabela gerenciatarefa.tbl_funcionarios
CREATE TABLE IF NOT EXISTS `tbl_funcionarios` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Nome` tinytext NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Tabela contendo todos os funcionários';

-- Copiando dados para a tabela gerenciatarefa.tbl_funcionarios: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `tbl_funcionarios` DISABLE KEYS */;
INSERT INTO `tbl_funcionarios` (`ID`, `Nome`) VALUES
	(1, 'Tiago');
/*!40000 ALTER TABLE `tbl_funcionarios` ENABLE KEYS */;

-- Copiando estrutura para tabela gerenciatarefa.tbl_tarefas
CREATE TABLE IF NOT EXISTS `tbl_tarefas` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Empresa` int(10) unsigned NOT NULL,
  `Funcionario` int(10) unsigned NOT NULL,
  `Status` tinyint(1) unsigned NOT NULL DEFAULT 0,
  `Assunto` tinytext NOT NULL,
  `DataInicial` date NOT NULL,
  `DataFinal` date DEFAULT NULL,
  `Prioridade` tinyint(3) unsigned DEFAULT 0,
  `Texto` text NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FKEmpresa` (`Empresa`),
  KEY `FKFuncionario` (`Funcionario`),
  CONSTRAINT `FKEmpresa` FOREIGN KEY (`Empresa`) REFERENCES `tbl_empresas` (`ID`),
  CONSTRAINT `FKFuncionario` FOREIGN KEY (`Funcionario`) REFERENCES `tbl_funcionarios` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='Tabela contendo todas as tarefas';

-- Copiando dados para a tabela gerenciatarefa.tbl_tarefas: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `tbl_tarefas` DISABLE KEYS */;
INSERT INTO `tbl_tarefas` (`ID`, `Empresa`, `Funcionario`, `Status`, `Assunto`, `DataInicial`, `DataFinal`, `Prioridade`, `Texto`) VALUES
	(1, 1, 1, 1, 'Criar Programa de Gerenciamento', '2018-01-25', NULL, 2, 'O programa deve ser criado como se fosse o gerenciador de tarefas do outlook.');
/*!40000 ALTER TABLE `tbl_tarefas` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
