-- MySQL dump 10.13  Distrib 8.0.19, for Linux (x86_64)
--
-- Host: localhost    Database: gym
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Abonements`
--

DROP TABLE IF EXISTS `Abonements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Abonements` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ID_Client` int DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `ID_Price` int DEFAULT NULL,
  `TotalPrice` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Abonements`
--

LOCK TABLES `Abonements` WRITE;
/*!40000 ALTER TABLE `Abonements` DISABLE KEYS */;
INSERT INTO `Abonements` VALUES (1,1,'2020-01-29','2020-02-05',1,500),(3,7,'2020-01-29','2020-02-05',2,210),(4,3,'2020-01-29','2020-02-05',2,210),(5,5,'2020-01-29','2020-04-15',1,3800),(6,8,'2020-02-03','2020-04-16',2,2160),(7,9,'2020-02-05','2020-04-30',2,2520),(8,10,'2020-02-05','2020-09-23',1,11500),(9,4,'2020-02-06','2020-02-29',3,550),(10,2,'2020-02-10','2020-02-29',2,540);
/*!40000 ALTER TABLE `Abonements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Clients` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `LastName` varchar(100) DEFAULT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `Gender` char(1) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'M',
  `DateOfBirth` date DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (1,'last','first','M','2020-01-01'),(2,'last','second','M','2019-09-05'),(3,'last','third','M','2020-01-03'),(4,'last','fourth','M','2017-06-13'),(5,'last','Fifth','M','2008-06-04'),(6,'Ololo','ahgsfahgafs','M','2019-03-07'),(7,'Abonement','aaaaaa','F','0001-01-01'),(8,'Last','Sixth','M','2020-01-01'),(9,'Group','Man','M','2000-11-07'),(10,'Single','Woman','F','1995-02-10');
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prices`
--

DROP TABLE IF EXISTS `Prices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Prices` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(100) DEFAULT NULL,
  `Cost` int NOT NULL DEFAULT '0',
  `MaxClients` int DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prices`
--

LOCK TABLES `Prices` WRITE;
/*!40000 ALTER TABLE `Prices` DISABLE KEYS */;
INSERT INTO `Prices` VALUES (1,'Single training',50,1),(2,'Group training',30,0),(3,'Child group training',25,10);
/*!40000 ALTER TABLE `Prices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Staffs`
--

DROP TABLE IF EXISTS `Staffs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Staffs` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `LastName` varchar(100) DEFAULT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `IsAdmin` tinyint(1) NOT NULL DEFAULT '0',
  `Login` varchar(100) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Staffs`
--

LOCK TABLES `Staffs` WRITE;
/*!40000 ALTER TABLE `Staffs` DISABLE KEYS */;
INSERT INTO `Staffs` VALUES (2,'admin','admin',1,'admin','8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918');
/*!40000 ALTER TABLE `Staffs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TrainGroups`
--

DROP TABLE IF EXISTS `TrainGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TrainGroups` (
  `ID_Training` int DEFAULT NULL,
  `ID_Client` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TrainGroups`
--

LOCK TABLES `TrainGroups` WRITE;
/*!40000 ALTER TABLE `TrainGroups` DISABLE KEYS */;
INSERT INTO `TrainGroups` VALUES (8,2),(9,10),(16,10),(13,10),(17,10),(18,10),(19,10),(20,10),(22,1),(23,3),(24,4);
/*!40000 ALTER TABLE `TrainGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Trainers`
--

DROP TABLE IF EXISTS `Trainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Trainers` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `LastName` varchar(100) DEFAULT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Trainers`
--

LOCK TABLES `Trainers` WRITE;
/*!40000 ALTER TABLE `Trainers` DISABLE KEYS */;
INSERT INTO `Trainers` VALUES (1,'Trainer','Bububu'),(2,'Train A','Aaazz'),(3,'Train B','ZZssa'),(4,'Rambo','John'),(5,'Worker','Paul'),(6,'Fisher','Sam'),(7,'McClaud','Konnor');
/*!40000 ALTER TABLE `Trainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Trainings`
--

DROP TABLE IF EXISTS `Trainings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Trainings` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ID_Trainer` int DEFAULT NULL,
  `ID_Price` int DEFAULT NULL,
  `ID_Creator` int DEFAULT NULL,
  `StartTime` datetime DEFAULT NULL,
  `EndTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Trainings`
--

LOCK TABLES `Trainings` WRITE;
/*!40000 ALTER TABLE `Trainings` DISABLE KEYS */;
INSERT INTO `Trainings` VALUES (1,2,1,1,'2020-02-03 07:00:00','2020-02-03 08:00:00'),(5,3,2,1,'2020-02-04 08:00:00','2020-02-04 09:00:00'),(8,6,2,1,'2020-02-05 07:00:00','2020-02-05 08:00:00'),(9,4,1,1,'2020-02-05 08:00:00','2020-02-05 09:00:00'),(10,7,2,1,'2020-02-05 09:00:00','2020-02-05 10:00:00'),(11,5,2,1,'2020-02-05 01:00:00','2020-02-05 02:00:00'),(12,5,3,1,'2020-02-05 11:00:00','2020-02-05 12:00:00'),(13,6,1,1,'2020-02-05 10:00:00','2020-02-05 11:00:00'),(14,1,1,1,'2020-02-05 04:00:00','2020-02-05 05:00:00'),(15,1,1,1,'2020-02-05 01:00:00','2020-02-05 02:00:00'),(16,1,1,1,'2020-02-05 03:00:00','2020-02-05 04:00:00'),(17,1,1,1,'2020-02-06 07:00:00','2020-02-06 08:00:00'),(18,1,1,1,'2020-02-05 12:00:00','2020-02-05 01:00:00'),(19,1,1,1,'2020-02-05 14:00:00','2020-02-05 15:00:00'),(20,1,1,1,'2020-02-05 16:00:00','2020-02-05 17:00:00'),(21,1,1,2,'2020-02-06 09:00:00',NULL),(22,1,1,2,'2020-02-06 08:00:00',NULL),(23,6,2,2,'2020-02-06 14:00:00',NULL),(24,5,3,2,'2020-02-06 11:00:00',NULL);
/*!40000 ALTER TABLE `Trainings` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-02-10  1:23:41
