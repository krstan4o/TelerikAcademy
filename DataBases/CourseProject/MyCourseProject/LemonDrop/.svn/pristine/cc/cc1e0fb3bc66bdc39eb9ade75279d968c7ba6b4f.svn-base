CREATE DATABASE  IF NOT EXISTS `supermarket` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `supermarket`;
-- MySQL dump 10.13  Distrib 5.6.11, for Win64 (x86_64)
--
-- Host: localhost    Database: supermarket
-- ------------------------------------------------------
-- Server version	5.6.11

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
-- Table structure for table `measures`
--

DROP TABLE IF EXISTS `measures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `measures` (
  `MeasureId` int(11) NOT NULL,
  `MeasureName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`MeasureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `measures`
--

LOCK TABLES `measures` WRITE;
/*!40000 ALTER TABLE `measures` DISABLE KEYS */;
INSERT INTO `measures` VALUES (1,'kilograms'),(2,'liters'),(3,'pieces'),(4,'grams'),(5,'mililiters');
/*!40000 ALTER TABLE `measures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products` (
  `ProductId` int(11) NOT NULL,
  `ProductName` varchar(45) DEFAULT NULL,
  `MeasureId` int(11) DEFAULT NULL,
  `VendorId` int(11) DEFAULT NULL,
  `BasePrice` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`ProductId`),
  KEY `MeasureId_idx` (`MeasureId`),
  KEY `VendorId_idx` (`VendorId`),
  CONSTRAINT `MeasureId` FOREIGN KEY (`MeasureId`) REFERENCES `measures` (`MeasureId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `VendorId` FOREIGN KEY (`VendorId`) REFERENCES `vendors` (`VendorId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'Tomattow',1,2,1),(2,'Chocolate',3,6,2),(3,'Beer \"Zagorka\"',2,17,2),(4,'BeerBlack \"Zagorka\"',2,17,2),(5,'Vodka \"Targovishte\"',2,7,8),(6,'Potato',1,2,2),(7,'Gum \"Orbit\"',3,10,1),(8,'Banana',1,5,3),(9,'Rakia \"Peshtera\"',5,11,4),(10,'Apples',1,5,1),(11,'Onion',1,5,1),(12,'Cabbage',1,2,3),(13,'Carrot',1,5,1),(14,'BeerBlack Ariana',1,9,2),(15,'Chips Hot \"Cheetos\"',3,12,2),(16,'Chips Tomato \"ChioChips\"',3,12,2),(17,'Mineral Watter \"Hisar\"',2,15,1),(18,'Beer \"Beks\"',1,14,2),(19,'Beer \"Tuborg\"',2,16,2),(20,'Menta \"Peshtera\"',2,11,2),(21,'Rakia \"Peshtera\"',2,11,5),(22,'Bread Tipov \"Dobrudja\"',3,4,1),(23,'Bread \"Dobrudja\"',3,4,1),(24,'Orange',1,1,1),(25,'Watermelon',1,1,2),(26,'Melon',1,1,2),(27,'Walnuts',1,1,2),(28,'Peanuts',1,1,2),(29,'Yogurt Normal \"Danone\"',5,3,1),(30,'Yogurt Strawberry \"Danone\"',5,3,2),(31,'Yogurt Peach\"Danone\"',5,3,2),(32,'Parsley',3,1,1),(33,'Lukanka \"Orehite\"',1,21,3),(34,'Salami \"Orehite\"',1,21,3),(35,'Fillet \"Orehite\"',1,21,6),(36,'CocaCola \"CocaCola\"',1,13,2),(37,'CocaCola \"Derby\"',1,22,1),(38,'Cofee Cola \"Derby\"',1,22,1),(39,'Lemon Cola \"Derby\"',1,22,1),(40,'Lemonade \"Derby\"',1,22,1),(41,'Orange Cola \"Derby\"',1,22,1),(42,'Apple Drink \"Derby\"',1,22,1),(43,'Tonic Dring \"Derby\"',1,22,1),(44,'Raspberry Cola \"Derby\"',1,22,1),(45,'Sprint Drink \"Derby\"',1,22,2),(46,'Pear Drink',1,22,2),(47,'Grapefruit \"Derby\"',1,22,2),(48,'Mastika \"Peshtera\"',2,11,5),(49,'Mastika \"Targovishte\"',2,7,8),(50,'Beer \"Ariana\"',2,9,2);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vendors`
--

DROP TABLE IF EXISTS `vendors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vendors` (
  `VendorId` int(11) NOT NULL,
  `VendorName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`VendorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vendors`
--

LOCK TABLES `vendors` WRITE;
/*!40000 ALTER TABLE `vendors` DISABLE KEYS */;
INSERT INTO `vendors` VALUES (1,'GreensOOD'),(2,'TraqnaAgro'),(3,'Danone'),(4,'DobrudjaLTD'),(5,'AgrisBulgaria'),(6,'Nestle Sofia Corp.'),(7,'Targovishte Bottling Company Ltd.'),(9,'Ariana Corp.'),(10,'Orbit Ltd.'),(11,'Peshtera OOD'),(12,'Cheetos Bulgaria'),(13,'CocaCola'),(14,'Beks'),(15,'Histar OOD'),(16,'Tuborg Ltd.'),(17,'Zagorka AD'),(18,'Kamenitza'),(19,'Fanta Ltd'),(20,'PepsiCo'),(21,'Orehite \"Bulgaria AD\"'),(22,'Derby Ltd');
/*!40000 ALTER TABLE `vendors` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2013-07-22 17:50:43
