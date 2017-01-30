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
-- Table structure for table `cad_hora_atendimento`
--

DROP TABLE IF EXISTS `cad_hora_atendimento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cad_hora_atendimento` (
  `dia_semana` int(11) NOT NULL,
  `cd_pes_juridica` varchar(128) NOT NULL,
  `hora_inicio` int(11) NOT NULL,
  `hora_fim` int(11) NOT NULL,
  PRIMARY KEY (`dia_semana`,`cd_pes_juridica`),
  KEY `ix_cd_pes_juridica2` (`cd_pes_juridica`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cad_hora_atendimento`
--

LOCK TABLES `cad_hora_atendimento` WRITE;
/*!40000 ALTER TABLE `cad_hora_atendimento` DISABLE KEYS */;
INSERT INTO `cad_hora_atendimento` VALUES (0,'482ee1df-afc2-492e-9745-531e8d940c83',8,22),(1,'482ee1df-afc2-492e-9745-531e8d940c83',10,23),(2,'482ee1df-afc2-492e-9745-531e8d940c83',10,23),(3,'482ee1df-afc2-492e-9745-531e8d940c83',10,23),(4,'482ee1df-afc2-492e-9745-531e8d940c83',10,23),(5,'482ee1df-afc2-492e-9745-531e8d940c83',10,23),(6,'482ee1df-afc2-492e-9745-531e8d940c83',8,22);
/*!40000 ALTER TABLE `cad_hora_atendimento` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-29 22:17:42
