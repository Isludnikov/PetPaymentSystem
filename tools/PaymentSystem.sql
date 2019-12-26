/*
Navicat MySQL Data Transfer

Source Server         : 192.168.33.10
Source Server Version : 80018
Source Host           : 192.168.33.10:3306
Source Database       : PaymentSystem

Target Server Type    : MYSQL
Target Server Version : 80018
File Encoding         : 65001

Date: 2019-12-26 18:06:48
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for MerchantIpRanges
-- ----------------------------
DROP TABLE IF EXISTS `MerchantIpRanges`;
CREATE TABLE `MerchantIpRanges` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MerchantId` int(11) NOT NULL,
  `IPRange` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_MerchantIpRanges_Merchants` (`MerchantId`),
  CONSTRAINT `FK_MerchantIpRanges_Merchants` FOREIGN KEY (`MerchantId`) REFERENCES `Merchants` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MerchantIpRanges
-- ----------------------------
INSERT INTO `MerchantIpRanges` VALUES ('1', '1', '127.0.0.0/24');
INSERT INTO `MerchantIpRanges` VALUES ('2', '1', '::1');

-- ----------------------------
-- Table structure for Merchants
-- ----------------------------
DROP TABLE IF EXISTS `Merchants`;
CREATE TABLE `Merchants` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Token` varchar(32) COLLATE utf8_unicode_ci NOT NULL,
  `SignKey` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `ShortName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FullName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_token` (`Token`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of Merchants
-- ----------------------------
INSERT INTO `Merchants` VALUES ('1', 'TestMerch', 'eVwboZvWzSfFejy5bkwqrXaTBWesBBUXcAQE1wSwl3c1WiyTKdmzsP5iacDnmQ2mxnu63S3gJxcL1UIrSgJ8M8kYunhNpApINamQRlXqN4FPLm7tkPg7GbwAy3MlZhXF4r5zfGgCxvq0YgHvme0vB7obtPC5sw6hsYd4erqf690pU5yWmTmCPOIu5cZ2ctl4Do3rYOOdISZjh05dQeUiWFcYdXAvUOtJMgSlRF8uJwp22Ut4LV9WbAoiVZWraw7', 'Test', 'Merchant for test purposes');
SET FOREIGN_KEY_CHECKS=1;
