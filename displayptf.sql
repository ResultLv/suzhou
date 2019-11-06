/*
 Navicat Premium Data Transfer

 Source Server         : 本地mysql
 Source Server Type    : MySQL
 Source Server Version : 50721
 Source Host           : localhost:3306
 Source Schema         : displayptf

 Target Server Type    : MySQL
 Target Server Version : 50721
 File Encoding         : 65001

 Date: 17/06/2019 09:49:41
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for company
-- ----------------------------
DROP TABLE IF EXISTS `company`;
CREATE TABLE `company`  (
  `CP_ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '公司编号，字符串类型',
  `CP_name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CP_address` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CP_tpnumber` int(24) NULL DEFAULT NULL COMMENT '公司电话号码',
  `CP_longitude` decimal(10, 7) NOT NULL COMMENT '经度',
  `CP_latitude` decimal(10, 7) NOT NULL COMMENT '纬度',
  `CP_joindate` date NULL DEFAULT NULL COMMENT '公司加入平台时间',
  PRIMARY KEY (`CP_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 66 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of company
-- ----------------------------
INSERT INTO `company` VALUES (1, '苏州协同智能制造创新中心', '江苏省苏州市吴中区吴中大道2888号', 68120482, 120.5528580, 31.2015870, '2018-03-11');
INSERT INTO `company` VALUES (2, '苏州联鑫模具有限公司', '苏州市吴中区横泾镇天鹅荡路2001号', 84653291, 120.5522100, 31.1883260, '2018-04-11');
INSERT INTO `company` VALUES (3, '苏州耀辉精密模具有限公司', '\r\n苏州市昆山市锦溪镇邵甸港路401号', 84632159, 121.1682300, 31.4691040, '2018-04-22');
INSERT INTO `company` VALUES (4, '苏州宝雅福隆电器科技有限公司', '苏州市吴中区北官渡路', 67423168, 120.6516650, 31.2524590, '2018-01-11');
INSERT INTO `company` VALUES (5, '苏州馨营兰电子科技有限公司', '苏州市吴江区吴变大道86号', 57298436, 120.6656920, 31.0852690, '2018-02-02');
INSERT INTO `company` VALUES (6, '苏州慧通汇创科技有限公司', '苏州市吴江区', 62468732, 120.5865720, 31.2166790, '2018-02-12');
INSERT INTO `company` VALUES (7, '苏州荣威模具有限公司', '苏州市吴中区苏蠡路65-6号', 81423696, 120.6124860, 31.2514070, '2018-02-26');
INSERT INTO `company` VALUES (8, '苏州立创精密模具科技有限公司', '苏州市吴中区兴东路18号', 69451287, 120.5511820, 31.1847590, '2017-12-11');
INSERT INTO `company` VALUES (9, '苏州智能制造设备有限公司', '苏州市吴中区', 57264834, 120.5900000, 31.2000000, '2017-11-02');
INSERT INTO `company` VALUES (10, '张家港中环海陆特锻股份有限公司', '张家港市锦丰镇合兴华山路', 57264834, 120.5960390, 31.9116310, '2017-12-21');
INSERT INTO `company` VALUES (11, '吴江兰天织造有限公司', '苏州市吴江区平盛路21号', 12345678, 120.6506200, 30.9392500, '2018-03-05');
INSERT INTO `company` VALUES (12, '苏州永捷电机有限公司', '苏州市吴中区胥口镇新峰路508号', 12345678, 120.4974700, 31.2228700, '2018-03-20');
INSERT INTO `company` VALUES (13, '常熟涤纶有限公司', '常熟市董浜镇董徐大道25号', 52671634, 120.9570000, 31.6665630, '2018-03-14');
INSERT INTO `company` VALUES (14, '常熟祥鑫汽配有限公司', '常熟市董浜镇望贤路', 12345678, 120.9222790, 31.6573010, '2018-04-03');
INSERT INTO `company` VALUES (15, '昆山微容电子企业有限公司', '江苏省昆山市正仪镇高新技术工业园', 12345678, 120.8820800, 31.3593200, '2018-07-04');
INSERT INTO `company` VALUES (16, '苏州市意可机电有限公司', '苏州市相城区黄埭镇春光路188号', 12345678, 120.5515400, 31.4440900, '2018-05-10');
INSERT INTO `company` VALUES (17, '苏州英创电力有限公司', '苏州', 12345678, 120.0000000, 31.0000000, '2018-08-16');
INSERT INTO `company` VALUES (18, '苏州益阳泰有限公司', '苏州', 12345678, 120.0000000, 31.0000000, '2018-04-12');
INSERT INTO `company` VALUES (19, '苏州富士胶片有限公司', '苏州', 12345678, 120.0000000, 31.0000000, '2018-03-01');
INSERT INTO `company` VALUES (20, '苏州利友有限公司', '苏州', 12345678, 120.0000000, 31.0000000, '2018-08-16');
INSERT INTO `company` VALUES (21, '苏州思维高科有限公司', '苏州', 12345678, 120.0000000, 31.0000000, '2018-08-16');
INSERT INTO `company` VALUES (22, '苏州伊斯丹实业有限公司', '太仓市双凤镇新湖建业路12号', 12345678, 121.0628300, 31.4744100, '2018-08-16');
INSERT INTO `company` VALUES (23, '苏州速腾电子科技有限公司', '苏州高新区泰山路2号43#', 12345678, 120.5422300, 31.3337500, '2018-10-09');
INSERT INTO `company` VALUES (24, '苏州考斯丹五金制品有限公司', '太仓市双凤镇新湖建业路', 12345678, 121.0628300, 31.4744100, '2018-06-06');
INSERT INTO `company` VALUES (25, '江苏华鹿纺织有限公司', '塘桥镇鹿苑金桥路20号', 12345678, 120.6390380, 31.8546700, '2018-02-21');
INSERT INTO `company` VALUES (26, '张家港天达工具有限公司', '张家港市大新镇大新村', 12345678, 120.5582430, 31.9717290, '2018-08-24');
INSERT INTO `company` VALUES (27, '苏州市富龙不锈钢链条厂', '苏州市相城区北桥街道灵峰村', 12345678, 120.6176830, 31.5298310, '2018-01-01');
INSERT INTO `company` VALUES (28, '苏州市富尔达科技股份有限公司', '太仓市双凤镇温州工业园', 12345678, 121.0558320, 31.4901490, '2019-05-31');
INSERT INTO `company` VALUES (29, '江苏华电吴江热电有限公司', '吴江平望镇平运路39号', 12345678, 120.6308950, 30.9735850, '2018-02-01');
INSERT INTO `company` VALUES (30, '苏州市吴江合众科技纤维有限公司', '吴江区平望镇中鲈开发区', 12345678, 120.6400380, 30.9738290, '2018-11-01');
INSERT INTO `company` VALUES (31, '鲜活果汁工业（昆山）有限公司', '江苏省苏州市昆山市源浦路500号', 12345678, 120.9674500, 31.2806400, '2018-09-12');
INSERT INTO `company` VALUES (32, '江苏普诺威电子股份有限公司（昆山）', '苏州市昆山市宏洋路322号', 12345678, 120.9890700, 31.2865300, '2018-07-11');
INSERT INTO `company` VALUES (33, '羽田电子科技（太仓）有限公司', '太仓市双凤镇黄桥村', 12345678, 121.0440830, 31.5049000, '2018-04-19');
INSERT INTO `company` VALUES (34, '苏州协鑫光伏科技有限公司', '苏州高新区五台山路69号', 12345678, 120.4356600, 31.3713700, '2018-03-14');
INSERT INTO `company` VALUES (35, '苏州勤堡精密机械有限公司', '苏州高新区真北路96号', 12345678, 120.4591200, 31.3837300, '2018-07-24');
INSERT INTO `company` VALUES (36, '江苏华神电子有限公司', '昆山市千灯镇宏洋路301号', 12345678, 120.9896900, 31.2853600, '2018-09-17');
INSERT INTO `company` VALUES (37, '张家港市云雾实业有限公司', '乐余镇兆丰人民路西首', 12345678, 120.7607120, 31.8829710, '2018-07-19');
INSERT INTO `company` VALUES (38, '绿点（苏州）科技有限公司', '苏州工业园区娄葑示范区扬清路7号', 12345678, 120.6587100, 31.3355000, '2018-06-20');
INSERT INTO `company` VALUES (39, '苏州中耀科技有限公司', '吴江市松陵镇友谊工业区胜信路368号', 12345678, 120.6541290, 31.0923400, '2018-09-17');
INSERT INTO `company` VALUES (40, '常熟市金华机械股份有限公司', '常熟市碧溪新区东张万和路25号', 12345678, 121.0157430, 31.7131790, '2018-09-20');
INSERT INTO `company` VALUES (41, '常熟市华德粉末冶金有限公司', '常熟市董浜镇华强路9号', 12345678, 120.9270000, 31.6490700, '2018-03-21');
INSERT INTO `company` VALUES (42, '苏州宇邦新型材料股份有限公司', '苏州吴中经济开发区越溪街道友翔路22号', 12345678, 120.5958480, 31.1873400, '2018-02-13');
INSERT INTO `company` VALUES (43, '天合汽车零部件（苏州）有限公司', '苏州相城经济开发区漕湖街道太东路2052号', NULL, 120.6533430, 31.3769210, '2018-09-27');
INSERT INTO `company` VALUES (44, '苏州市利来星辰塑业科技有限公司', '苏州工业园区胜浦镇江浦路40号', NULL, 120.8555500, 31.3114600, '2018-08-17');
INSERT INTO `company` VALUES (45, '昆山万源通电子科技有限公司', '昆山市巴城镇石牌中华路1288号', NULL, 120.9181600, 31.5015100, '2019-04-10');
INSERT INTO `company` VALUES (46, '昆山广谦电子有限公司', '昆山市巴城镇石牌中华路1288号', NULL, 120.9181600, 31.5015100, '2019-04-10');
INSERT INTO `company` VALUES (47, '内德史罗夫紧固件（昆山）有限公司', '江苏省昆山市锦溪镇锦东路368号', NULL, 120.9507700, 31.1804800, '2019-04-11');
INSERT INTO `company` VALUES (48, '太仓博泽汽车部件有限公司', '太仓港经济技术开发区新区广州路188号', NULL, 121.1014730, 31.4986552, '2019-04-09');
INSERT INTO `company` VALUES (49, '太仓南雁新能源传动有限公司', '太仓市浏河镇观海路1号  215431', 53833982, 121.2720748, 31.5171315, '2019-05-15');
INSERT INTO `company` VALUES (50, '太仓立日包装容器有限公司', '太仓市璜泾工业园友谊路18号', 53827628, 121.1012708, 31.6873874, '2019-05-15');
INSERT INTO `company` VALUES (51, '太仓荣文合成纤维有限公司', '江苏省太仓市璜泾镇荣文路13号', 53818873, 121.1180138, 31.6499191, '2019-01-23');
INSERT INTO `company` VALUES (52, '吴江市兴业纺织有限公司', '盛泽镇南环二路1998号', 63511870, 120.6496650, 30.8830415, '2019-05-10');
INSERT INTO `company` VALUES (53, '吴江市双盈化纺实业有限公司', '吴江区盛泽镇黄家溪村215228', 63951078, 120.6379885, 30.9057826, '2019-04-28');
INSERT INTO `company` VALUES (54, '吴江越泽经编纺织有限公司', '江苏省苏州市吴江区盛泽镇圣塘村11组', NULL, 120.6330920, 30.8621983, '2019-03-15');
INSERT INTO `company` VALUES (55, '吴江飞翔经编纺织有限公司', '江苏省苏州市吴江区盛泽镇圣塘村11组', NULL, 120.6403705, 30.8911617, '2019-04-29');
INSERT INTO `company` VALUES (56, '苏州高求美达橡胶金属减震科技有限公司', '苏州市吴江区黎里镇汾越路1088号', NULL, 120.7985099, 31.0218890, '2019-03-07');
INSERT INTO `company` VALUES (57, '克莱斯电梯（中国）有限公司', '江苏省苏州市吴江区七都镇230省道北侧', 82060307, 120.4059689, 30.9484273, '2019-03-26');
INSERT INTO `company` VALUES (58, '代尔塔（中国）安全防护有限公司', '苏州市吴江区平望镇中鲈工业集中区欧盛大道2号', 63647000, 120.6371302, 31.0267252, '2019-02-27');
INSERT INTO `company` VALUES (59, '苏州嘉友吉星汽车零部件有限公司', '阳澄湖镇岸山村西横港街26号2幢', 66831339, 120.7175103, 31.5175368, '2019-02-28');
INSERT INTO `company` VALUES (60, '张家港特恩驰电缆有限公司', '江苏省苏州市张家港市锦丰镇创业路12号', 55398289, 120.6198574, 31.9621908, '2019-03-01');
INSERT INTO `company` VALUES (61, '瓦克化学气相二氧化硅（张家港）有限公司', '扬子江国际化工园区长江路78号', 81642020, 120.4673615, 31.9856568, '2019-03-08');
INSERT INTO `company` VALUES (62, '张家港富瑞重型装备有限公司', '江苏扬子江重型装备工业园朝东圩港路1号', 58982271, 120.5434498, 31.9949909, '2019-05-14');
INSERT INTO `company` VALUES (63, '张家港市逸洋精密轴承有限公司', '江苏省张家港市塘桥镇妙桥镇友谊路', 58435933, 120.6756685, 31.8094638, '2019-02-26');
INSERT INTO `company` VALUES (64, '瑞铁机床（苏州）股份有限公司', '江苏省苏州市太仓城厢镇科技产业园区胜泾路178号', 80607632, 121.1005233, 31.4233990, '2019-05-14');
INSERT INTO `company` VALUES (65, '张家港市亨昌焊材有限公司', '苏州市张家港市凤凰镇港口', 58488402, 120.6720042, 31.7462138, '2019-02-25');

-- ----------------------------
-- Table structure for cp_eqp
-- ----------------------------
DROP TABLE IF EXISTS `cp_eqp`;
CREATE TABLE `cp_eqp`  (
  `CP_ID` int(11) NOT NULL COMMENT '公司编号',
  `EQP_ID` int(255) NOT NULL COMMENT '设备编号',
  `CE_number` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`CP_ID`, `EQP_ID`) USING BTREE,
  INDEX `CE_EQP_ID`(`EQP_ID`) USING BTREE,
  CONSTRAINT `CE_CP_ID` FOREIGN KEY (`CP_ID`) REFERENCES `company` (`CP_ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `CE_EQP_ID` FOREIGN KEY (`EQP_ID`) REFERENCES `equipment` (`EQP_ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of cp_eqp
-- ----------------------------
INSERT INTO `cp_eqp` VALUES (1, 1, NULL);
INSERT INTO `cp_eqp` VALUES (1, 4, NULL);
INSERT INTO `cp_eqp` VALUES (1, 5, NULL);
INSERT INTO `cp_eqp` VALUES (1, 11, 1);
INSERT INTO `cp_eqp` VALUES (1, 12, 1);
INSERT INTO `cp_eqp` VALUES (1, 13, 1);
INSERT INTO `cp_eqp` VALUES (1, 14, 1);
INSERT INTO `cp_eqp` VALUES (1, 15, NULL);
INSERT INTO `cp_eqp` VALUES (1, 16, NULL);
INSERT INTO `cp_eqp` VALUES (2, 3, 1);
INSERT INTO `cp_eqp` VALUES (2, 7, 1);
INSERT INTO `cp_eqp` VALUES (2, 9, 1);
INSERT INTO `cp_eqp` VALUES (3, 2, 1);
INSERT INTO `cp_eqp` VALUES (3, 10, 1);
INSERT INTO `cp_eqp` VALUES (4, 6, 1);
INSERT INTO `cp_eqp` VALUES (5, 8, 1);

-- ----------------------------
-- Table structure for eqp_md
-- ----------------------------
DROP TABLE IF EXISTS `eqp_md`;
CREATE TABLE `eqp_md`  (
  `EQP_ID` int(11) NOT NULL COMMENT '设备编号',
  `MD_ID` int(11) NOT NULL COMMENT '模具编号',
  `No_off` bit(1) NULL DEFAULT NULL COMMENT '开闭标记',
  `Spindle_Speed` double NULL DEFAULT NULL COMMENT '主轴转速',
  `Coordinate` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '坐标精度',
  `Alarm` bit(1) NULL DEFAULT NULL COMMENT '报警标记',
  `MD_Progress` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`EQP_ID`, `MD_ID`) USING BTREE,
  INDEX `ME_MD_ID`(`MD_ID`) USING BTREE,
  CONSTRAINT `ME_EQP_ID` FOREIGN KEY (`EQP_ID`) REFERENCES `equipment` (`EQP_ID`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `ME_MD_ID` FOREIGN KEY (`MD_ID`) REFERENCES `mould` (`MD_ID`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of eqp_md
-- ----------------------------
INSERT INTO `eqp_md` VALUES (1, 2, b'0', 1600, NULL, NULL, 100);
INSERT INTO `eqp_md` VALUES (2, 4, b'1', 1400, NULL, NULL, 60);
INSERT INTO `eqp_md` VALUES (3, 3, b'1', 1700, NULL, NULL, 40);
INSERT INTO `eqp_md` VALUES (4, 1, b'1', 1800, NULL, NULL, 80);
INSERT INTO `eqp_md` VALUES (5, 5, b'1', 1800, NULL, NULL, 90);
INSERT INTO `eqp_md` VALUES (6, 6, NULL, NULL, NULL, NULL, 66);
INSERT INTO `eqp_md` VALUES (7, 7, NULL, NULL, NULL, NULL, 74);
INSERT INTO `eqp_md` VALUES (8, 8, NULL, NULL, NULL, NULL, 18);
INSERT INTO `eqp_md` VALUES (9, 9, NULL, NULL, NULL, NULL, 52);
INSERT INTO `eqp_md` VALUES (10, 10, NULL, NULL, NULL, NULL, 49);
INSERT INTO `eqp_md` VALUES (11, 11, NULL, NULL, NULL, NULL, 93);
INSERT INTO `eqp_md` VALUES (12, 12, NULL, NULL, NULL, NULL, 81);
INSERT INTO `eqp_md` VALUES (13, 13, NULL, NULL, NULL, NULL, 26);

-- ----------------------------
-- Table structure for equipment
-- ----------------------------
DROP TABLE IF EXISTS `equipment`;
CREATE TABLE `equipment`  (
  `EQP_ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '设备编号',
  `EQP_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `EQP_brand` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备品牌',
  `EQP_model` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备型号',
  `EQP_Dateofmf` date NULL DEFAULT NULL COMMENT '设备生产日期',
  `EQP_orgin` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备产地',
  `EQP_category` varchar(5) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备种类',
  `EQP_offon` bit(1) NULL DEFAULT NULL COMMENT '设备启停状态',
  `EQP_state` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备运行状态',
  `EQP_process` int(11) NULL DEFAULT NULL COMMENT '设备加工进度',
  `EQP_time` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备加工时间',
  PRIMARY KEY (`EQP_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of equipment
-- ----------------------------
INSERT INTO `equipment` VALUES (1, '智能切割机', '西门子', 'MP8-30', '2006-03-01', '德国', 'EDM', b'0', '待机中', 0, '');
INSERT INTO `equipment` VALUES (2, '精密火花机', '发那科', 'ZNC750', '2008-03-05', '中国', 'EDW', b'1', '运行中', 60, '1小时30分');
INSERT INTO `equipment` VALUES (3, '数控机床', '发那科', 'CG52', '2013-06-09', '广州', 'CNC', b'0', '暂停中', 80, '2小时40分');
INSERT INTO `equipment` VALUES (4, '慢走丝切割机', '沙迪克', 'VL400QS', '2018-04-06', '日本', 'EDM', b'1', '运行中', 40, '45分');
INSERT INTO `equipment` VALUES (5, '火花机', '沙迪克', 'VL500QS', '2018-04-10', '日本', 'EDW', b'0', '', NULL, '');
INSERT INTO `equipment` VALUES (6, '快走丝线切割', '西门子', 'MP8-40', '2018-04-09', '德国', 'EDM', b'1', '运行中', 90, '3小时');
INSERT INTO `equipment` VALUES (7, '智能数控机床', '西门子', 'MMP-80', '2018-04-11', '德国', 'CNC', b'1', '运行中', 30, '30分');
INSERT INTO `equipment` VALUES (8, '智能数控机床', '沙迪克', 'VLS-600', '2018-04-13', '日本', 'CNC', b'0', '暂停中', 50, '1小时');
INSERT INTO `equipment` VALUES (9, '精密火花机', '发那科', 'ZNC650', '2018-03-10', '苏州', 'EDW', b'1', '运行中', 70, '1小时20分');
INSERT INTO `equipment` VALUES (10, '数控机床', '西门子', 'MP6-40', '2018-04-04', '德国', 'CNC', b'1', '运行中', 10, '20分');
INSERT INTO `equipment` VALUES (11, '切割机', '沙迪克', 'AD30LSDK1', '2018-01-06', '日本', 'EDM', b'1', '运行中', 60, '2小时');
INSERT INTO `equipment` VALUES (12, '切割机', '沙迪克', 'AD30LSDK2', '2018-01-06', '日本', 'EDM', b'1', '运行中', 80, '2小时20分');
INSERT INTO `equipment` VALUES (13, '切割机', '沙迪克', 'AD30LSDK3', '2018-01-06', '日本', 'EDM', b'1', '运行中', 40, '1小时15分');
INSERT INTO `equipment` VALUES (14, '切割机', '沙迪克', 'AD30LSDK4', '2018-01-06', '日本', 'EDM', b'0', '待机中', NULL, '');
INSERT INTO `equipment` VALUES (15, '切割机', '沙迪克', 'AD30LSDK5', '2018-01-06', '日本', 'EDM', b'0', '待机中', NULL, '');
INSERT INTO `equipment` VALUES (16, '火花机', '沙迪克', 'ALN400Qs', '2018-01-06', '日本', 'EDW', b'0', '', NULL, '');

-- ----------------------------
-- Table structure for mould
-- ----------------------------
DROP TABLE IF EXISTS `mould`;
CREATE TABLE `mould`  (
  `MD_ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '模具编号',
  `MD_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `MD_drawing_ID` int(11) NULL DEFAULT NULL COMMENT '模具图纸编号',
  `MD_program_ID` int(11) NULL DEFAULT NULL COMMENT '模具生产程序',
  `MDS_offtime` datetime(0) NULL DEFAULT NULL COMMENT '模具开模时间',
  `MDS_ontime` datetime(0) NULL DEFAULT NULL COMMENT '模具比模时间',
  `MDS_address` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MDS_prodt_sum` int(11) NULL DEFAULT NULL COMMENT '模具生产的产品数量',
  `MDS_mtifo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模具维修信息',
  PRIMARY KEY (`MD_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of mould
-- ----------------------------
INSERT INTO `mould` VALUES (1, 'md1', 1, 1, '2018-04-04 00:00:00', '2018-04-07 00:00:00', '武汉', NULL, NULL);
INSERT INTO `mould` VALUES (2, 'md2', 1, 3, '2018-04-05 00:00:00', '2018-04-06 00:00:00', '苏州', NULL, NULL);
INSERT INTO `mould` VALUES (3, 'md3', 2, 4, '2018-04-09 00:00:00', '2018-04-14 00:00:00', '苏州', NULL, NULL);
INSERT INTO `mould` VALUES (4, 'md4', 4, 2, '2018-04-11 00:00:00', '2018-04-18 00:00:00', '杭州', NULL, NULL);
INSERT INTO `mould` VALUES (5, 'md5', 3, 2, '2018-04-07 00:00:00', '2018-04-12 00:00:00', '北京', NULL, NULL);
INSERT INTO `mould` VALUES (6, 'md6', 2, 4, '2018-04-14 00:00:00', '2018-04-16 00:00:00', '上海', NULL, NULL);
INSERT INTO `mould` VALUES (7, 'md7', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (8, 'md8', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (9, 'md9', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (10, 'md10', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (11, 'md11', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (12, 'md12', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (13, 'md13', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `mould` VALUES (14, 'md14', NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for od_md
-- ----------------------------
DROP TABLE IF EXISTS `od_md`;
CREATE TABLE `od_md`  (
  `OD_ID` int(11) NOT NULL COMMENT '订单编号',
  `MD_ID` int(11) NOT NULL COMMENT '模具编号',
  `OM_number` int(11) NULL DEFAULT NULL COMMENT '模具数量',
  PRIMARY KEY (`OD_ID`, `MD_ID`) USING BTREE,
  INDEX `OM_MD_ID`(`MD_ID`) USING BTREE,
  CONSTRAINT `OM_MD_ID` FOREIGN KEY (`MD_ID`) REFERENCES `mould` (`MD_ID`) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT `OM_OD_ID` FOREIGN KEY (`OD_ID`) REFERENCES `orders` (`OD_ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of od_md
-- ----------------------------
INSERT INTO `od_md` VALUES (1, 2, NULL);
INSERT INTO `od_md` VALUES (1, 3, NULL);
INSERT INTO `od_md` VALUES (2, 4, NULL);
INSERT INTO `od_md` VALUES (3, 1, NULL);
INSERT INTO `od_md` VALUES (3, 5, NULL);
INSERT INTO `od_md` VALUES (4, 6, NULL);
INSERT INTO `od_md` VALUES (5, 7, NULL);
INSERT INTO `od_md` VALUES (6, 8, NULL);
INSERT INTO `od_md` VALUES (7, 9, NULL);
INSERT INTO `od_md` VALUES (8, 10, NULL);
INSERT INTO `od_md` VALUES (9, 11, NULL);
INSERT INTO `od_md` VALUES (10, 12, NULL);

-- ----------------------------
-- Table structure for orders
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders`  (
  `OD_ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '订单编号',
  `Customer_ID` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户标记',
  `OD_compstaus` int(11) NULL DEFAULT NULL COMMENT '订单完成进度',
  `OD_value` decimal(10, 0) NULL DEFAULT NULL COMMENT '订单金额',
  `CP_ID` int(11) NOT NULL COMMENT '公司ID',
  `OD_date` datetime(0) NULL DEFAULT NULL COMMENT '订单下单日期',
  `OD_state` int(11) NULL DEFAULT NULL COMMENT '订单是否执行中',
  PRIMARY KEY (`OD_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of orders
-- ----------------------------
INSERT INTO `orders` VALUES (1, '苏州慧通汇创科技有限公司', 70, 500001, 3, '2018-06-11 00:00:00', 1);
INSERT INTO `orders` VALUES (2, '苏州立创精密模具科技有限公司', 60, 500000000, 2, '2018-08-23 00:00:00', 1);
INSERT INTO `orders` VALUES (3, '苏州智能制造设备有限公司', 20, 400, 2, '2018-10-06 00:00:00', 1);
INSERT INTO `orders` VALUES (4, '苏州科瑞流体有限公司', 70, 600, 1, '2018-10-07 00:00:00', 1);
INSERT INTO `orders` VALUES (5, '苏州利友有限公司', 40, 800, 4, '2018-06-30 00:00:00', 1);
INSERT INTO `orders` VALUES (6, '苏州联鑫模具有限公司', 90, 1000, 5, '2018-11-03 00:00:00', 1);
INSERT INTO `orders` VALUES (7, '苏州馨营兰电子科技有限公司', 60, 1200, 8, '2018-12-24 00:00:00', 1);
INSERT INTO `orders` VALUES (8, '苏州耀辉精密模具有限公司', 80, 1400, 6, '2019-02-16 00:00:00', 1);
INSERT INTO `orders` VALUES (9, '苏州协同智能制造创新中心', 40, 1600, 1, '2019-03-01 00:00:00', 1);
INSERT INTO `orders` VALUES (10, '苏州荣威模具有限公司', 50, 1800, 3, '2019-03-06 00:00:00', 1);

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `User_ID` int(11) NOT NULL AUTO_INCREMENT,
  `User_email` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `User_pword` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `User_typer` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `User_name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`User_ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES (1, '1122@qq.com', 'admin', 'admin', 'admin');

SET FOREIGN_KEY_CHECKS = 1;
