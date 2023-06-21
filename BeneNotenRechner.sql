CREATE DATABASE  IF NOT EXISTS `benenotenrechner_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `benenotenrechner_db`;
-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: benenotenrechner_db
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_grade`
--

DROP TABLE IF EXISTS `tbl_grade`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_grade` (
  `grade_id` int NOT NULL AUTO_INCREMENT,
  `grade` varchar(64) NOT NULL,
  `date` datetime NOT NULL,
  `name` varchar(45) NOT NULL,
  `evaluation` float NOT NULL DEFAULT '1',
  `id_subject` int NOT NULL,
  PRIMARY KEY (`grade_id`),
  KEY `fk_grade_idx` (`id_subject`),
  CONSTRAINT `fk_grade` FOREIGN KEY (`id_subject`) REFERENCES `tbl_subject` (`subject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=188 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_grade`
--

LOCK TABLES `tbl_grade` WRITE;
/*!40000 ALTER TABLE `tbl_grade` DISABLE KEYS */;
INSERT INTO `tbl_grade` VALUES (173,'QÛ´u®8woB¤ÝÝ¸hX','2023-06-20 00:00:00','1ZP',1,24),(174,'1JÈÉôåZfµxÓºsuº','2023-06-21 00:00:00','2ZP',1,24),(175,')B|5â[Î×vpût9','2023-06-21 00:00:00','3ZP',1,24),(176,'rGcÚª2BP\ZVu','2023-06-21 00:00:00','1ZP',1,25),(177,'QÛ´u®8woB¤ÝÝ¸hX','2023-06-21 00:00:00','2ZP',1,25),(178,'kÚým(×ôÚpqh^','2023-06-21 00:00:00','3ZP',1,25),(179,'ÅrÕü8Rñ VÚ\r_','2023-06-21 00:00:00','1ZP',1,26),(180,'z¾^+¼î\nMyÆ´Ù&','2023-06-21 00:00:00','2ZP',1,26),(181,'`LfÆ¦Ç~ÍßQç','2023-06-21 00:00:00','3ZP',1,26),(182,'QÛ´u®8woB¤ÝÝ¸hX','2023-06-21 00:00:00','1ZP',1,27),(183,'QÛ´u®8woB¤ÝÝ¸hX','2023-06-21 00:00:00','2ZP',1,27),(184,'\nD3è¹¤ïQÑt\Z ¦g','2023-06-21 00:00:00','1ZP',1,30),(185,')B|5â[Î×vpût9','2023-06-21 00:00:00','1ZP',1,28),(186,'\nD3è¹¤ïQÑt\Z ¦g','2023-06-21 00:00:00','2ZP',1,28),(187,'1JÈÉôåZfµxÓºsuº','2023-06-21 00:00:00','1ZP',1,29);
/*!40000 ALTER TABLE `tbl_grade` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_profile`
--

DROP TABLE IF EXISTS `tbl_profile`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_profile` (
  `profile_id` int NOT NULL AUTO_INCREMENT,
  `index` int NOT NULL,
  `id_users` int NOT NULL,
  PRIMARY KEY (`profile_id`),
  KEY `id_user_idx` (`id_users`),
  CONSTRAINT `id_user` FOREIGN KEY (`id_users`) REFERENCES `tbl_users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_profile`
--

LOCK TABLES `tbl_profile` WRITE;
/*!40000 ALTER TABLE `tbl_profile` DISABLE KEYS */;
INSERT INTO `tbl_profile` VALUES (5,1,9);
/*!40000 ALTER TABLE `tbl_profile` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_subject`
--

DROP TABLE IF EXISTS `tbl_subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_subject` (
  `subject_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `id_supersubject` int NOT NULL,
  PRIMARY KEY (`subject_id`),
  KEY `fk_subject_idx` (`id_supersubject`),
  CONSTRAINT `fk_subject` FOREIGN KEY (`id_supersubject`) REFERENCES `tbl_supersubject` (`supersubject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_subject`
--

LOCK TABLES `tbl_subject` WRITE;
/*!40000 ALTER TABLE `tbl_subject` DISABLE KEYS */;
INSERT INTO `tbl_subject` VALUES (24,'M117',10),(25,'M122',10),(26,'M293',10),(27,'M320',10),(28,'ENG',11),(29,'MAT',11),(30,'ABU',12);
/*!40000 ALTER TABLE `tbl_subject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_supersubject`
--

DROP TABLE IF EXISTS `tbl_supersubject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_supersubject` (
  `supersubject_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `id_profile` int NOT NULL,
  PRIMARY KEY (`supersubject_id`),
  KEY `fk_profile_idx` (`id_profile`),
  CONSTRAINT `fk_profile` FOREIGN KEY (`id_profile`) REFERENCES `tbl_profile` (`profile_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_supersubject`
--

LOCK TABLES `tbl_supersubject` WRITE;
/*!40000 ALTER TABLE `tbl_supersubject` DISABLE KEYS */;
INSERT INTO `tbl_supersubject` VALUES (10,'Informatik Fächer',5),(11,'Allgemeine Fächer',5),(12,'ABU',5);
/*!40000 ALTER TABLE `tbl_supersubject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_users`
--

DROP TABLE IF EXISTS `tbl_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(256) NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_users`
--

LOCK TABLES `tbl_users` WRITE;
/*!40000 ALTER TABLE `tbl_users` DISABLE KEYS */;
INSERT INTO `tbl_users` VALUES (9,'BrokenMesh','7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31');
/*!40000 ALTER TABLE `tbl_users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-06-21  8:04:22
