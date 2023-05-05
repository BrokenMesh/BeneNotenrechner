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
  `id_subject` int NOT NULL,
  PRIMARY KEY (`grade_id`),
  KEY `fk_grade_idx` (`id_subject`),
  CONSTRAINT `fk_grade` FOREIGN KEY (`id_subject`) REFERENCES `tbl_subject` (`subject_id`)
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_grade`
--

LOCK TABLES `tbl_grade` WRITE;
/*!40000 ALTER TABLE `tbl_grade` DISABLE KEYS */;
INSERT INTO `tbl_grade` VALUES (1,5.5,'2023-05-05 11:02:18',4),(2,5,'2023-05-05 11:02:18',4),(3,4.5,'2023-05-05 11:02:18',4),(4,3.5,'2023-05-05 11:02:18',4),(5,1,'2023-05-05 11:02:18',4),(6,5.5,'2023-05-05 11:02:18',5),(7,5,'2023-05-05 11:02:18',5),(8,4.5,'2023-05-05 11:02:18',5),(9,3.5,'2023-05-05 11:02:18',5),(10,1,'2023-05-05 11:02:18',5),(11,5.5,'2023-05-05 11:02:18',6),(12,5,'2023-05-05 11:02:18',6),(13,4.5,'2023-05-05 11:02:18',6),(14,3.5,'2023-05-05 11:02:18',6),(15,1,'2023-05-05 11:02:18',6),(16,5.5,'2023-05-05 11:02:18',7),(17,5,'2023-05-05 11:02:18',7),(18,4.5,'2023-05-05 11:02:18',7),(19,3.5,'2023-05-05 11:02:18',7),(20,1,'2023-05-05 11:02:18',7),(21,5.5,'2023-05-05 11:02:18',8),(22,5,'2023-05-05 11:02:18',8),(23,4.5,'2023-05-05 11:02:18',8),(24,3.5,'2023-05-05 11:02:18',8),(25,1,'2023-05-05 11:02:18',8),(26,5.5,'2023-05-05 11:02:18',9),(27,5,'2023-05-05 11:02:18',9),(28,4.5,'2023-05-05 11:02:18',9),(29,3.5,'2023-05-05 11:02:18',9),(30,1,'2023-05-05 11:02:18',9),(31,5.5,'2023-05-05 11:02:18',10),(32,5,'2023-05-05 11:02:18',10),(33,4.5,'2023-05-05 11:02:18',10),(34,3.5,'2023-05-05 11:02:18',10),(35,1,'2023-05-05 11:02:18',10),(36,5.5,'2023-05-05 11:02:18',11),(37,5,'2023-05-05 11:02:18',11),(38,4.5,'2023-05-05 11:02:18',11),(39,3.5,'2023-05-05 11:02:18',11),(40,1,'2023-05-05 11:02:18',11),(41,5.5,'2023-05-05 11:02:18',12),(42,5,'2023-05-05 11:02:18',12),(43,4.5,'2023-05-05 11:02:18',12),(44,3.5,'2023-05-05 11:02:18',12),(45,1,'2023-05-05 11:02:18',12);
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
  `id_users` int NOT NULL,
  PRIMARY KEY (`profile_id`),
  KEY `id_user_idx` (`id_users`),
  CONSTRAINT `id_user` FOREIGN KEY (`id_users`) REFERENCES `tbl_users` (`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_profile`
--

LOCK TABLES `tbl_profile` WRITE;
/*!40000 ALTER TABLE `tbl_profile` DISABLE KEYS */;
INSERT INTO `tbl_profile` VALUES (1,3);
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_subject`
--

LOCK TABLES `tbl_subject` WRITE;
/*!40000 ALTER TABLE `tbl_subject` DISABLE KEYS */;
INSERT INTO `tbl_subject` VALUES (4,'ABU',1),(5,'M122',1),(6,'M320',1),(7,'ABU',2),(8,'M122',2),(9,'M320',2),(10,'ABU',3),(11,'M122',3),(12,'M320',3);
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
  `semester` int NOT NULL,
  `id_profile` int NOT NULL,
  PRIMARY KEY (`supersubject_id`),
  KEY `fk_profile_idx` (`id_profile`),
  CONSTRAINT `fk_profile` FOREIGN KEY (`id_profile`) REFERENCES `tbl_profile` (`profile_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_supersubject`
--

LOCK TABLES `tbl_supersubject` WRITE;
/*!40000 ALTER TABLE `tbl_supersubject` DISABLE KEYS */;
INSERT INTO `tbl_supersubject` VALUES (1,'Demo1',1,1),(2,'Demo2',1,1),(3,'Demo3',1,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_users`
--

LOCK TABLES `tbl_users` WRITE;
/*!40000 ALTER TABLE `tbl_users` DISABLE KEYS */;
INSERT INTO `tbl_users` VALUES (2,'Hicham','7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31'),(3,'BrokenMesh','7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31'),(4,'User','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4');
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

-- Dump completed on 2023-05-05 11:07:43
