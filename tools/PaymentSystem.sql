/*
Navicat MySQL Data Transfer

Source Server         : 192.168.33.10
Source Server Version : 80018
Source Host           : 192.168.33.10:3306
Source Database       : PaymentSystem

Target Server Type    : MYSQL
Target Server Version : 80018
File Encoding         : 65001

Date: 2019-12-28 19:29:48
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Merchant
-- ----------------------------
DROP TABLE IF EXISTS `Merchant`;
CREATE TABLE `Merchant` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Token` varchar(32) COLLATE utf8_unicode_ci NOT NULL,
  `SignKey` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `ShortName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FullName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Active` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_token` (`Token`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Merchant
-- ----------------------------
INSERT INTO `Merchant` VALUES ('1', 'TestMerchantWithLongToken', 'eVwboZvWzSfFejy5bkwqrXaTBWesBBUXcAQE1wSwl3c1WiyTKdmzsP5iacDnmQ2mxnu63S3gJxcL1UIrSgJ8M8kYunhNpApINamQRlXqN4FPLm7tkPg7GbwAy3MlZhXF4r5zfGgCxvq0YgHvme0vB7obtPC5sw6hsYd4erqf690pU5yWmTmCPOIu5cZ2ctl4Do3rYOOdISZjh05dQeUiWFcYdXAvUOtJMgSlRF8uJwp22Ut4LV9WbAoiVZWraw7', 'Test', 'Merchant for test purposes', '1');

-- ----------------------------
-- Table structure for MerchantIpRange
-- ----------------------------
DROP TABLE IF EXISTS `MerchantIpRange`;
CREATE TABLE `MerchantIpRange` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MerchantId` int(11) NOT NULL,
  `IPRange` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_MerchantIpRanges_Merchants` (`MerchantId`),
  CONSTRAINT `FK_MerchantIpRanges_Merchants` FOREIGN KEY (`MerchantId`) REFERENCES `Merchant` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MerchantIpRange
-- ----------------------------
INSERT INTO `MerchantIpRange` VALUES ('1', '1', '127.0.0.0/24');
INSERT INTO `MerchantIpRange` VALUES ('2', '1', '::1');

-- ----------------------------
-- Table structure for Operation
-- ----------------------------
DROP TABLE IF EXISTS `Operation`;
CREATE TABLE `Operation` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ExternalId` char(15) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `SessionId` int(11) NOT NULL,
  `ProcessingId` int(11) NOT NULL,
  `Amount` bigint(10) NOT NULL,
  `InvolvedAmount` bigint(10) NOT NULL,
  `OperationType` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_OperationId` (`ExternalId`) USING BTREE,
  KEY `FK_Operation_Session` (`SessionId`),
  KEY `FK_Operation_Processing` (`ProcessingId`),
  CONSTRAINT `FK_Operation_Processing` FOREIGN KEY (`ProcessingId`) REFERENCES `Processing` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Operation_Session` FOREIGN KEY (`SessionId`) REFERENCES `Session` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Operation
-- ----------------------------

-- ----------------------------
-- Table structure for Processing
-- ----------------------------
DROP TABLE IF EXISTS `Processing`;
CREATE TABLE `Processing` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProcessingName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Processing
-- ----------------------------

-- ----------------------------
-- Table structure for Session
-- ----------------------------
DROP TABLE IF EXISTS `Session`;
CREATE TABLE `Session` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ExternalId` char(24) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `OrderId` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `MerchantId` int(11) NOT NULL,
  `Amount` bigint(10) NOT NULL,
  `Currency` char(3) COLLATE utf8_unicode_ci NOT NULL,
  `OrderDescription` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FormKey` varchar(16) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `FormLanguage` char(3) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_SessionIs` (`ExternalId`) USING BTREE,
  UNIQUE KEY `IX_Merchant_OrderId` (`MerchantId`,`OrderId`) USING BTREE,
  CONSTRAINT `FK_Session_Merchant` FOREIGN KEY (`MerchantId`) REFERENCES `Merchant` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Session
-- ----------------------------
INSERT INTO `Session` VALUES ('2', 'KPH7aVDEY0y5ugIykLN4rQ==', '212321', '1', '100', 'EUR', null, null, 'RUS');
INSERT INTO `Session` VALUES ('3', '9Qc3EA10G0K3RsHJ4sQaGQ==', '212322', '1', '100', 'EUR', null, null, 'RUS');
INSERT INTO `Session` VALUES ('4', 'xG9r9aC/9E+iXqbKeRn2hA==', '212323', '1', '100', 'EUR', null, null, 'RUS');
SET FOREIGN_KEY_CHECKS=1;
