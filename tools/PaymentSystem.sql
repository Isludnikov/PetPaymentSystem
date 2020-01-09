/*
Navicat MySQL Data Transfer

Source Server         : 192.168.33.10
Source Server Version : 80018
Source Host           : 192.168.33.10:3306
Source Database       : PaymentSystem

Target Server Type    : MYSQL
Target Server Version : 80018
File Encoding         : 65001

Date: 2020-01-09 18:59:47
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
  `TerminalId` int(11) NOT NULL,
  `Amount` bigint(10) NOT NULL,
  `InvolvedAmount` bigint(10) NOT NULL,
  `OperationType` int(11) NOT NULL,
  `Status` int(11) NOT NULL,
  `ProcessingOrderId` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_OperationId` (`ExternalId`) USING BTREE,
  KEY `FK_Operation_Session` (`SessionId`),
  KEY `FK_Operation_Terminal` (`TerminalId`),
  CONSTRAINT `FK_Operation_Session` FOREIGN KEY (`SessionId`) REFERENCES `Session` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Operation_Terminal` FOREIGN KEY (`TerminalId`) REFERENCES `Terminal` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Operation
-- ----------------------------
INSERT INTO `Operation` VALUES ('2', '157858910400000', '26', '1', '10000', '10000', '0', '2', '57f3fc58-7e8a-4174-ae4e-3a063eede8291', '2020-01-09 16:58:24');

-- ----------------------------
-- Table structure for Processing
-- ----------------------------
DROP TABLE IF EXISTS `Processing`;
CREATE TABLE `Processing` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProcessingName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `LibraryName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Namespace` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Processing
-- ----------------------------
INSERT INTO `Processing` VALUES ('1', 'Test', 'TestProcessing.dll', 'TestProcessing.TestProcessing');

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
  `ExpireTime` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_SessionIs` (`ExternalId`) USING BTREE,
  UNIQUE KEY `IX_Merchant_OrderId` (`MerchantId`,`OrderId`) USING BTREE,
  CONSTRAINT `FK_Session_Merchant` FOREIGN KEY (`MerchantId`) REFERENCES `Merchant` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Session
-- ----------------------------
INSERT INTO `Session` VALUES ('18', 'GaEw0+wb4ky20Mxv/zKH6w==', '212329', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 14:47:09');
INSERT INTO `Session` VALUES ('19', 'spRUbakrsUiwAPP2w2mU7A==', '212330', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:05:13');
INSERT INTO `Session` VALUES ('20', 'd1hIGgPoZ0SQtyktt9tM9Q==', '212331', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:11:13');
INSERT INTO `Session` VALUES ('21', '2m+JxAbI8EGBUaZVN1bVfw==', '212332', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:14:24');
INSERT INTO `Session` VALUES ('22', 'wYFsF9V7O0mVoKarOYaDmw==', '212333', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:17:01');
INSERT INTO `Session` VALUES ('23', 'ca/oVVv48kurcGYcsRUZ7Q==', '212334', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:21:51');
INSERT INTO `Session` VALUES ('24', 'YF/Yv+/tzEOt/yn8UlKQjw==', '212335', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-08 17:31:29');
INSERT INTO `Session` VALUES ('25', 'dODRc4kdxE+jcIGFHUR4JQ==', '212337', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-09 19:25:07');
INSERT INTO `Session` VALUES ('26', 'pqHDCB/TAUidPwXpLhR9mQ==', '212338', '1', '10000', 'EUR', null, null, 'RUS', '2020-01-09 19:28:24');

-- ----------------------------
-- Table structure for Terminal
-- ----------------------------
DROP TABLE IF EXISTS `Terminal`;
CREATE TABLE `Terminal` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ProcessingId` int(11) NOT NULL,
  `Priority` int(11) NOT NULL DEFAULT '0',
  `MerchantId` int(11) NOT NULL,
  `Rule` varchar(1024) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FinalRule` tinyint(1) NOT NULL DEFAULT '0',
  `NextOnError` tinyint(1) NOT NULL DEFAULT '0',
  `Active` tinyint(1) NOT NULL DEFAULT '0',
  `Login` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccessToken` varchar(2048) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Terminal_Merchant` (`MerchantId`),
  KEY `FK_Terminal_Processing` (`ProcessingId`),
  CONSTRAINT `FK_Terminal_Merchant` FOREIGN KEY (`MerchantId`) REFERENCES `Merchant` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Terminal_Processing` FOREIGN KEY (`ProcessingId`) REFERENCES `Processing` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Terminal
-- ----------------------------
INSERT INTO `Terminal` VALUES ('1', 'TestTerminal', '1', '0', '1', null, '1', '0', '1', null, null, null);
SET FOREIGN_KEY_CHECKS=1;
