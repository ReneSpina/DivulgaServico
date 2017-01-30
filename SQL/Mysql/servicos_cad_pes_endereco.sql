-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: localhost    Database: servicos
-- ------------------------------------------------------
-- Server version	5.7.11-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `cad_pes_endereco`
--

DROP TABLE IF EXISTS `cad_pes_endereco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cad_pes_endereco` (
  `cd_pessoa` varchar(128) NOT NULL,
  `sq_endereco` int(11) NOT NULL AUTO_INCREMENT,
  `nm_cidade` varchar(255) DEFAULT NULL,
  `nm_logradouro` varchar(255) DEFAULT NULL,
  `nm_bairro` varchar(255) DEFAULT NULL,
  `nm_estado` varchar(255) DEFAULT NULL,
  `cd_cep` varchar(20) NOT NULL,
  `numero` int(11) NOT NULL,
  `localizacao` longtext,
  PRIMARY KEY (`cd_pessoa`,`sq_endereco`),
  KEY `ix_tmp_autoinc` (`sq_endereco`),
  KEY `ix_cd_pessoa` (`cd_pessoa`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cad_pes_endereco`
--

LOCK TABLES `cad_pes_endereco` WRITE;
/*!40000 ALTER TABLE `cad_pes_endereco` DISABLE KEYS */;
INSERT INTO `cad_pes_endereco` VALUES ('482ee1df-afc2-492e-9745-531e8d940c83',2,'* TRIAL * T','Avenida Industrial','NULL','SP','* TRIAL *',600,'POINT (-23.6486246 -46.532941999999991)');
/*!40000 ALTER TABLE `cad_pes_endereco` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-29 22:17:43
