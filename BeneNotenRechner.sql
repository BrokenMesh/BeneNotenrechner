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
  `grade` float NOT NULL,
  `date` datetime NOT NULL,
  `name` varchar(45) NOT NULL,
  `evaluation` float NOT NULL DEFAULT '1',
  `id_subject` int NOT NULL,
  PRIMARY KEY (`grade_id`),
  KEY `fk_grade_idx` (`id_subject`),
  CONSTRAINT `fk_grade` FOREIGN KEY (`id_subject`) REFERENCES `tbl_subject` (`subject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_grade`
--

LOCK TABLES `tbl_grade` WRITE;
/*!40000 ALTER TABLE `tbl_grade` DISABLE KEYS */;
INSERT INTO `tbl_grade` VALUES (56,6,'2023-05-26 00:00:00','1ZP',1,6),(139,5,'2023-05-14 00:00:00','1ZP',1,14),(141,6,'2023-05-14 00:00:00','1ZP',1,21),(143,5.3,'2023-05-26 00:00:00','1ZP',1,16),(154,5.6,'2023-05-26 00:00:00','1ZP',1,19),(156,5.7,'2023-05-26 00:00:00','1ZP',1,20),(159,6,'2023-05-26 00:00:00','1ZP',1,18),(160,5.3,'2023-05-26 00:00:00','2ZP',1,18),(161,5.5,'2023-05-26 00:00:00','3ZP',1,18),(162,6,'2023-05-26 00:00:00','2ZP',1,19),(163,4.9,'2023-05-26 00:00:00','3ZP',1,19),(164,4.8,'2023-05-26 00:00:00','2ZP',1,20),(165,5.8,'2023-05-26 00:00:00','3ZP',1,20),(166,5.2,'2023-05-26 00:00:00','1ZP',1,22),(167,5.7,'2023-05-26 00:00:00','2ZP',1,22),(168,6,'2023-05-26 00:00:00','3ZP',1,22),(169,5.5,'2023-05-26 00:00:00','1ZP',1,17),(170,5,'2023-05-26 00:00:00','2ZP',1,17);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_profile`
--

LOCK TABLES `tbl_profile` WRITE;
/*!40000 ALTER TABLE `tbl_profile` DISABLE KEYS */;
INSERT INTO `tbl_profile` VALUES (1,1,3),(2,1,6),(3,1,7);
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
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_subject`
--

LOCK TABLES `tbl_subject` WRITE;
/*!40000 ALTER TABLE `tbl_subject` DISABLE KEYS */;
INSERT INTO `tbl_subject` VALUES (6,'M320',1),(14,'ABU',3),(16,'MAT',2),(17,'ENG',2),(18,'M117',1),(19,'M122',1),(20,'M293',1),(21,'ABU2',8),(22,'M164',1);
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_supersubject`
--

LOCK TABLES `tbl_supersubject` WRITE;
/*!40000 ALTER TABLE `tbl_supersubject` DISABLE KEYS */;
INSERT INTO `tbl_supersubject` VALUES (1,'Informatik Fächer',1),(2,'Allgemeine Fächer',1),(3,'ABU ',1),(8,'Informatik Fächer',2);
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_users`
--

LOCK TABLES `tbl_users` WRITE;
/*!40000 ALTER TABLE `tbl_users` DISABLE KEYS */;
INSERT INTO `tbl_users` VALUES (2,'Hicham','7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31'),(3,'BrokenMesh','7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31'),(4,'User','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4'),(6,'asd','688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6'),(7,'User2','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');
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

-- Dump completed on 2023-05-26  9:45:18
