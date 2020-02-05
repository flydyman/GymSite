-- MySQL dump 10.13  Distrib 5.7.28, for Linux (x86_64)
--
-- Host: localhost    Database: testdb3
-- ------------------------------------------------------
-- Server version	5.7.28

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
-- Table structure for table `Abonements`
--

DROP TABLE IF EXISTS `Abonements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Abonements` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_Client` int(11) DEFAULT NULL,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `ID_Price` int(11) DEFAULT NULL,
  `TotalPrice` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Abonement_FK` (`ID_Client`),
  KEY `Abonement_FK_1` (`ID_Price`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Abonements`
--

LOCK TABLES `Abonements` WRITE;
/*!40000 ALTER TABLE `Abonements` DISABLE KEYS */;
INSERT INTO `Abonements` VALUES (1,3,'2020-01-29','2020-04-08',2,241500),(2,12,'2020-01-30','2020-01-30',1,5),(3,4,'2020-01-31','2020-04-23',1,4100);
/*!40000 ALTER TABLE `Abonements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Clients` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(45) DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `Gender` char(1) DEFAULT 'M',
  `DateOfBirth` date DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (4,'Mananoga','Fidele','F','2006-05-17'),(5,'Mananoga','Fidele','F','2001-07-10'),(6,'Ololo','Lalala','M','2020-01-01'),(7,'qqqq','wwwww','M','2020-01-02'),(8,'aaaa','zzzz','M','2020-01-06'),(9,'xxxx','zzzz','M','2020-01-27'),(10,'xxxx','zzzz','M','2020-01-27'),(11,'xxxx','zzzz','M','2020-01-27'),(12,'zaq','qaz','M','2019-12-29');
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prices`
--

DROP TABLE IF EXISTS `Prices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Prices` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(100) DEFAULT NULL,
  `Cost` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prices`
--

LOCK TABLES `Prices` WRITE;
/*!40000 ALTER TABLE `Prices` DISABLE KEYS */;
INSERT INTO `Prices` VALUES (1,'Single training',50),(2,'Group training',35),(3,'Child training',25);
/*!40000 ALTER TABLE `Prices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Staffs`
--

DROP TABLE IF EXISTS `Staffs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Staffs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(45) DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `IsAdmin` tinyint(4) DEFAULT '0',
  `Login` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Staffs`
--

LOCK TABLES `Staffs` WRITE;
/*!40000 ALTER TABLE `Staffs` DISABLE KEYS */;
INSERT INTO `Staffs` VALUES (1,'Root','User',1,'root','root');
/*!40000 ALTER TABLE `Staffs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TrainGroups`
--

DROP TABLE IF EXISTS `TrainGroups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `TrainGroups` (
  `ID_Training` int(11) NOT NULL,
  `ID_Client` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID_Training`),
  KEY `Groups_FK_1` (`ID_Client`),
  CONSTRAINT `Groups_FK` FOREIGN KEY (`ID_Training`) REFERENCES `Trainings` (`ID`),
  CONSTRAINT `Groups_FK_1` FOREIGN KEY (`ID_Client`) REFERENCES `Clients` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TrainGroups`
--

LOCK TABLES `TrainGroups` WRITE;
/*!40000 ALTER TABLE `TrainGroups` DISABLE KEYS */;
/*!40000 ALTER TABLE `TrainGroups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Trainers`
--

DROP TABLE IF EXISTS `Trainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Trainers` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LastName` varchar(45) DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Trainers`
--

LOCK TABLES `Trainers` WRITE;
/*!40000 ALTER TABLE `Trainers` DISABLE KEYS */;
INSERT INTO `Trainers` VALUES (1,'Trainer','First'),(2,'Trainer','Second');
/*!40000 ALTER TABLE `Trainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Trainings`
--

DROP TABLE IF EXISTS `Trainings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Trainings` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ID_Trainer` int(11) DEFAULT NULL,
  `ID_Price` int(11) DEFAULT NULL,
  `ID_Creator` int(11) DEFAULT NULL,
  `StartTime` datetime DEFAULT NULL,
  `EndTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Calendar_FK` (`ID_Creator`),
  KEY `Calendar_FK_1` (`ID_Trainer`),
  KEY `Calendar_FK_2` (`ID_Price`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Trainings`
--

LOCK TABLES `Trainings` WRITE;
/*!40000 ALTER TABLE `Trainings` DISABLE KEYS */;
/*!40000 ALTER TABLE `Trainings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'testdb3'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-02-02 10:19:20
