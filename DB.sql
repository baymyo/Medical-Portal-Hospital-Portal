--
-- Definition of table `album`
--

DROP TABLE IF EXISTS `album`;
CREATE TABLE `album` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `kategoriid` varchar(8) DEFAULT NULL,
  `adi` varchar(75) DEFAULT NULL,
  `etiket` varchar(100) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `uye` tinyint(1) DEFAULT NULL,
  `yorum` tinyint(1) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_albumid` (`kategoriid`) USING BTREE,
  FULLTEXT KEY `index_albumtext` (`adi`,`etiket`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `album`
--

/*!40000 ALTER TABLE `album` DISABLE KEYS */;
/*!40000 ALTER TABLE `album` ENABLE KEYS */;


--
-- Definition of table `anket`
--

DROP TABLE IF EXISTS `anket`;
CREATE TABLE `anket` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `anketid` bigint(20) DEFAULT NULL,
  `adi` varchar(100) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  `resimurl` varchar(50) NOT NULL,
  `grup` varchar(15) NOT NULL,
  `yatay` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `index_anketid` (`anketid`)
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `anket`
--

/*!40000 ALTER TABLE `anket` DISABLE KEYS */;
INSERT INTO `anket` (`id`,`anketid`,`adi`,`kayittarihi`,`aktif`,`resimurl`,`grup`,`yatay`) VALUES 
 (9,0,'Test Anket Sorusu Girildi','2015-01-06 12:14:56',1,'test-anket-sorusu-girildi-125514-42dbc7.jpg','True',1),
 (10,9,'Test Cevap 3','2015-01-06 12:14:56',0,'test-cevap-3-122615-12fe6f.jpg','C Grubu',0),
 (11,9,'Test Cevap 2','2015-01-06 12:14:56',0,'test-cevap-2-120415-192ef6.jpg','B Grubu',0),
 (12,9,'Test Cevap 1','2015-01-06 12:14:56',0,'test-cevap-1-125514-f61620.jpg','A Grubu',0),
 (13,9,'Aslı Senem ÖZKAN','2015-01-10 00:10:04',0,'asli-senem-ozkan-000410-5179a8.jpg','C Grubu',0),
 (14,9,'Sezgin Zühtü ÖZKAN','2015-01-10 00:10:04',0,'sezgin-zuhtu-ozkan-000410-de1db5.jpg','C Grubu',0),
 (15,9,'Gökhan GÖKPINAR','2015-01-10 00:10:04',0,'gokhan-gokpinar-000410-237929.jpg','A Grubu',0);
/*!40000 ALTER TABLE `anket` ENABLE KEYS */;


--
-- Definition of table `anketpuan`
--

DROP TABLE IF EXISTS `anketpuan`;
CREATE TABLE `anketpuan` (
  `id` varchar(36) NOT NULL,
  `hesapid` varchar(36) DEFAULT NULL,
  `anketid` bigint(20) DEFAULT NULL,
  `soruid` bigint(20) DEFAULT NULL,
  `ip` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_anketpuanid` (`hesapid`,`anketid`,`soruid`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `anketpuan`
--

/*!40000 ALTER TABLE `anketpuan` DISABLE KEYS */;
INSERT INTO `anketpuan` (`id`,`hesapid`,`anketid`,`soruid`,`ip`) VALUES 
 ('d79f91f7-bc1e-4231-bbc8-8fdeb06ca347',NULL,9,10,'::1');
/*!40000 ALTER TABLE `anketpuan` ENABLE KEYS */;


--
-- Definition of table `astroloji`
--

DROP TABLE IF EXISTS `astroloji`;
CREATE TABLE `astroloji` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `burcid` smallint(6) DEFAULT NULL,
  `icerik` varchar(1000) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `astroloji`
--

/*!40000 ALTER TABLE `astroloji` DISABLE KEYS */;
INSERT INTO `astroloji` (`id`,`hesapid`,`burcid`,`icerik`,`kayittarihi`,`yoneticionay`,`aktif`) VALUES 
 (15,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',1,'Bugün özellikle sevgilinize karşı düşüncesiz davranışlarda bulunabilirsiniz. İlişkinize zarar verebilecek bu davranışları, iradeniz ve kontrolünüz sayesinde önlemeye çalışın. İşinizle ilgili bazı şeyleri fazla kuruntu yapmayın. Kafanız hep bu kuruntularla dolu oluyor, başka şeylere odaklanamıyorsunuz. Her darda kaldığınızda ailenizin destek olması müsrif harcamalar yapmanıza yol açıyor. Dikkat, bu destek kesilebilir! Bir süredir ihmal ettiğiniz sağlık problemleriyle ilgilenmek için uygun bir gün.','2014-12-19 00:00:00',1,1),
 (16,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',2,' İlişkinizle ilgili almanız gereken ciddi bir kararı sürekli erteliyorsunuz. Duygularınızı dinleyip en uygun kararı verin. Tam istediğiniz gibi bir durum ortaya çıkacak. Çalışma hayatındaki titizlik ve düzen takıntınız çevrenizdekileri bıktırıyor. Hem kendinize, hem de başkalarına karşı hoşgörülü olmaya çalışın. Ekonomik durumunuz gittikçe iyileşiyor, siz yine de tedbiri elden bırakmayın. Beslenmenize dikkat etmemeniz, fiziksel performansınızın düşmesine neden oluyor.','2014-12-19 00:00:00',1,1),
 (17,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',3,'Aşk yaşamınızda küçük şeyleri kafanıza takıyor ve sorun yapıyorsunuz. Duygusal konularda ani kararlar almayın ve esnek olun. Mesleğinizde kendiniz dışında gelişen olaylara müdahale etmekten vazgeçmelisiniz. Bu sizi yıpratabilir. Gereksiz yere aldığınız sorumlulukların altında ezilebilirsiniz. Aldığınız sorumlulukları yerine getirebilir, başarılı olabilirsiniz ama emeğinizin karşılığını görmeniz gecikecek. Ertelediğiniz borçlar birikti! Sağlıkla ilgili küçük bir endişe yaşayabilirsiniz','2014-12-19 00:00:00',1,1),
 (18,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',4,'Aşırıya kaçan kıskançlık huyunuz, sevgilinize bir süredir rahatsızlık veriyor. Endişeyi ve öfkeyi bir kenara bırakıp olumlu düşünürseniz, aşk hayatınızdaki sorunları atlatabilir, hayal ettiğiniz ilişkiyi yaşayabilirsiniz. Abartılı iyi niyetiniz ve arkadaş canlısı oluşunuz iş yerinizde kullanılıyor ve başkaları adına çıkışlarınız göze batıyor. Bu davranışlar sizin düşman kazanmanıza neden olabilir. Hiç ummadığınız yerden gelen para sizi çok sevindirecek. Gaz sancıları yaşamak istemiyorsanız, yediklerinize dikkat edin','2014-12-19 00:00:00',1,1),
 (19,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',5,'Partnerinizle aranızdaki küçük bir sorun gitgide büyümeye devam ediyor. Bu konuda üzerinize düşeni tam olarak yapamadığınızı düşünüyorsunuz. Kendinizi yıpratmak yerine, sevgilinizle vakit geçirmeye çalışın. İşini çok seviyorsunuz, ama işlerinizdeki durgunluk sizi üzüyor, başka bir alanda şans aramayı bile düşünüyorsunuz. Bugünlerde yeni girişimler ve atılımlar olabilir. Para konusunda tutumlu olun. Sonra sıkıntıyı siz çekersiniz. Uykunuzdan ödün vermeyin, yoksa ertesi günden hiç verim alamazsınız.','2014-12-19 00:00:00',1,1),
 (20,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',6,'Sevgilinizin, beraberliğiniz hakkındaki fikirleri, üzerinizde olumsuz etki yapıyor ve karar almanızı zorlaştırıyor. Kararlarınızda mantığınızın sesine kulak verin. İş hayatında beklediğiniz atılım için uygun bir gün değil. Biraz daha beklemeniz gerekiyor. İş yerinde yakın geçmişte yaşadığınız olayın etkisinden kurtulmanız, canlı, hareketli yaşamınıza dönmelisiniz. Parayla ilgili konularda gecikmeler söz konusu. İhmal ettiğiniz bir rahatsızlığınız keyfinizi kaçıracak gibi.','2014-12-19 00:00:00',1,1),
 (21,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',7,'Sevgiliniz, artık bağlılığınızdan kuşkuya düşüyor. Eskisi kadar güven duymadığını bugün dile getirebilir. Sorunları elinizden geldiğince düzeltmeye bakın. Bugünlerde kendinizi işe fazla kaptırdınız. Fazla sorumluluk yükleniyorsunuz. İşkolikliğiniz bir şeylerden kaçışın göstergesi gibi. Sınırlı dikkatli harcamanıza rağmen ölçülü, hesaplı yaşamak zorunda kalıyorsunuz. Midenize çok düşkünsünüz. Hep yemek istiyorsunuz, biraz rejim yapın yoksa kilolarla başınız derde girecek','2014-12-19 00:00:00',1,1),
 (22,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',8,'Beraberliğiniz hala canlı olmasını iki tarafında akıllıca davranmasına borçlu. İşinizle ilgili sorunların üzerine gidip, onları çözmeye çalışmaktansa, kendinizi dinleyip depresyona girmeyi tercih ediyorsunuz. Bu da sizi kontrol altına alıp etkisiz hale getirmeye çalışan insanların ekmeğine yağ sürüyor. Maddi konularla ilgili sorunlar keyfinizi kaçırabilir. Sinirlerinizin gerginliği sırt ve boyunda kasılmalara ve ağrılara yol açıyor. Gevşetici egzersizlere ihtiyacınız var.','2014-12-19 00:00:00',1,1),
 (23,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',9,'Aşka olan küskünlüğünüz son bulmak üzere. Çok yakında yeni bir ilişkinin kapıları aralanabilir. İş hayatınızda gerginlikler yaşayabilirsiniz. Tedbirli ve dikkatli olmanıza rağmen beklenmedik gelişmeler olabilir. Daha sakin olmaya ve kafanızı küçük detaylarla çok fazla meşgul etmemeye özen göstermelisiniz. Para sorunlarınızı çözmekte zorlanıyorsunuz, yakınlarınızdan destek isteyin. Bir akrabanızın sağlık sorunu, nedeniyle kısa bir yolculuk yapabilirsiniz.','2014-12-19 00:00:00',1,1),
 (24,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',10,'Bugün, kendi düşüncelerinizi savunmak zorunda kalabilirsiniz. Sabit fikirler içinde olduğunuz zaman, olaylara dar bir çerçeveden bakıyorsunuz. Yapacağınız konuşmalarda takıntılı davranmaktan vazgeçmelisiniz.','2014-12-19 00:00:00',1,1),
 (25,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',11,'Yeni tanıştığınız biri size kederli anılarınızı unutturacak. Fakat herkese karşı kuşku içindesiniz. Unutmayın ki, bu kişi sizde hoş duygular uyandıracak ve yaşam enerjinizin yükselmesini sağlayacak! Çalışma temponuzda bir artış var. Daha çok kariyerinizle ilgili çalışmalar yapacağınız ve sorumluluklarınızı en iyi şekilde yerine getireceğiniz bu günü değerlendirin. Maddi yatırımlar için başkalarını dinlemeyin. Uykusuzluk ve yetersiz beslenme bağışıklık sisteminizi zayıflatıyor.','2014-12-19 00:00:00',1,1),
 (26,'bcd470f2-eb2d-4002-9ef2-d1132183bf65',12,'Geçmişle ilgili hala kopamadığınız bağı, uzun zamandır zorluyor, koparmaya çalışıyorsunuz. Artık sizinle ilgilenenleri fark edin! Yeni bir şey yaşamadan güvensizlikten kurtulabileceğinizi sanmamalısınız. İşinizdeki hareketlilik, sizi hem çok meşgul edecek hem de yoracak. Herkesin işine koşmaya çalışmak bugünlerde size ağır gelebilir. Parasal çalışmalar yaparken elinize geçen fırsatları değerlendirmekte zorlanabilirsiniz. Stres yaratan işlerden uzak durun','2014-12-19 00:00:00',1,1);
/*!40000 ALTER TABLE `astroloji` ENABLE KEYS */;


--
-- Definition of table `firma`
--

DROP TABLE IF EXISTS `firma`;
CREATE TABLE `firma` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `kategoriid` varchar(4) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `adi` varchar(75) DEFAULT NULL,
  `yetkili` varchar(40) DEFAULT NULL,
  `adres` varchar(100) DEFAULT NULL,
  `mail` varchar(60) DEFAULT NULL,
  `web` varchar(60) DEFAULT NULL,
  `telefon1` varchar(16) DEFAULT NULL,
  `telefon2` varchar(16) DEFAULT NULL,
  `gsm` varchar(16) DEFAULT NULL,
  `sehir` varchar(20) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_firmaid` (`kategoriid`),
  FULLTEXT KEY `index_firmatext` (`adi`,`sehir`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `firma`
--

/*!40000 ALTER TABLE `firma` DISABLE KEYS */;
/*!40000 ALTER TABLE `firma` ENABLE KEYS */;


--
-- Definition of table `galeri`
--

DROP TABLE IF EXISTS `galeri`;
CREATE TABLE `galeri` (
  `id` varchar(36) NOT NULL,
  `albumid` bigint(20) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `aciklama` varchar(500) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `kapak` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_galeriid` (`albumid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `galeri`
--

/*!40000 ALTER TABLE `galeri` DISABLE KEYS */;
/*!40000 ALTER TABLE `galeri` ENABLE KEYS */;


--
-- Definition of table `gosterim`
--

DROP TABLE IF EXISTS `gosterim`;
CREATE TABLE `gosterim` (
  `id` varchar(36) NOT NULL,
  `hesapid` varchar(36) DEFAULT NULL,
  `ip` varchar(15) DEFAULT NULL,
  `modulid` varchar(15) DEFAULT NULL,
  `icerikid` varchar(50) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_gosterimid` (`hesapid`,`modulid`,`icerikid`(40))
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `gosterim`
--

/*!40000 ALTER TABLE `gosterim` DISABLE KEYS */;
INSERT INTO `gosterim` (`id`,`hesapid`,`ip`,`modulid`,`icerikid`,`kayittarihi`) VALUES 
 ('026948e4-d1cd-4a4d-8c40-8b3894c030dc','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','profil','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','2014-12-25 15:48:40'),
 ('71a0a704-435d-44db-8f19-252e5bcc3143','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','4','2015-01-03 12:05:29'),
 ('0e6475b3-aa7a-4ea2-b1de-7283808cefd0','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','5','2015-01-03 12:01:41'),
 ('e1bacc17-4867-4761-8a0b-1603e96a01d0','','::1','haber','4','2015-01-03 08:42:33'),
 ('26111985-ca00-4321-9cb1-4bcd805a5a86','','::1','haber','5','2014-12-31 15:51:17'),
 ('0c5f83de-129e-4a8d-ae35-c5fa8be45051','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','4','2014-12-31 14:35:31'),
 ('a40884bc-edca-4b63-8371-379156149d15','','::1','haber','3','2014-12-31 14:26:49'),
 ('c8b7fd88-34ff-49f2-836d-1bf4ee4a2a27','','::1','haber','3','2014-12-31 14:21:45'),
 ('ef5c1a9f-fd64-4f81-9364-f325690c855f','','::1','haber','3','2014-12-31 14:18:33'),
 ('d85aa176-4b4e-461e-b1df-c32934124c08','','::1','haber','3','2014-12-31 14:15:30'),
 ('59026046-efcc-48ed-984b-2a810e4917d2','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','3','2014-12-31 12:17:51'),
 ('32a44396-67a2-4882-8417-59e79c8372bb','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','3','2014-12-31 12:03:49'),
 ('e6ed8058-5ecf-4963-92f6-e87721baa969','2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','haber','3','2014-12-31 12:08:17'),
 ('811e0cb7-47e6-4dfb-8fe7-962eff478b98','','::1','haber','4','2015-01-07 00:34:38'),
 ('9722fae0-5d8c-4228-9538-a676e517f12d','','::1','haber','3','2015-01-07 00:47:41'),
 ('4823d50a-25cc-47cf-a3e6-1006d9481815','','::1','haber','5','2015-01-08 23:52:06'),
 ('e38edded-cc6b-49fe-98cc-e9af8dfeec25','','::1','haber','3','2015-01-30 22:24:05');
/*!40000 ALTER TABLE `gosterim` ENABLE KEYS */;


--
-- Definition of table `haber`
--

DROP TABLE IF EXISTS `haber`;
CREATE TABLE `haber` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `kategoriid` varchar(25) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `baslik` varchar(75) DEFAULT NULL,
  `ozet` varchar(150) DEFAULT NULL,
  `icerik` longtext CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `etiket` varchar(100) DEFAULT NULL,
  `sehir` varchar(20) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `video` bigint(20) DEFAULT NULL,
  `galeri` bigint(20) DEFAULT NULL,
  `uye` tinyint(1) DEFAULT '0',
  `yorum` tinyint(1) DEFAULT '1',
  `yoneticionay` tinyint(1) DEFAULT '0',
  `anasayfa` tinyint(1) DEFAULT '1',
  `aktif` tinyint(1) unsigned DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `index_haberid` (`kategoriid`),
  FULLTEXT KEY `index_habertext` (`baslik`,`etiket`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `haber`
--

/*!40000 ALTER TABLE `haber` DISABLE KEYS */;
INSERT INTO `haber` (`id`,`hesapid`,`kategoriid`,`resimurl`,`baslik`,`ozet`,`icerik`,`etiket`,`sehir`,`kayittarihi`,`guncellemetarihi`,`gosterimsayi`,`video`,`galeri`,`uye`,`yorum`,`yoneticionay`,`anasayfa`,`aktif`) VALUES 
 (4,'2538bb2e-dec7-4eab-bafc-24a66f23d2ed','0001','a-ve-vatanina-duskun-bir-halktir-141133-8fc358.jpg','HATAY HALKI BAYRAĞINA VE VATANINA DÜŞKÜN BİR HALKTIR','MEHTERAN VE İSTİKLAL MARŞI EŞLİĞİNDE TÜRK BAYRAĞI GÖKLERE ÇEKİLDİBAŞKAN SAVAŞ HATAY HALKI BAYRAĞINA VE VATANINA DÜŞKÜN BİR HALKTIR','<p>\r\n	Hatay B&uuml;y&uuml;kşehir Belediye Başkanı Do&ccedil;.Dr.L&uuml;tf&uuml; Savaş, Aşağıoba Mahallesi Muhtarı Necmettin G&uuml;ney tarafından d&uuml;zenlenen 10 kilo ağırlığında ve 50 metrekare b&uuml;y&uuml;kl&uuml;ğ&uuml;ndeki T&uuml;rk Bayrağı&rsquo;nı g&ouml;ndere &ccedil;ekmek i&ccedil;in d&uuml;zenlenen t&ouml;rene katıldı.</p>\r\n<p>\r\n	Hatay B&uuml;y&uuml;kşehir Belediyesi Mehteran Takımı&rsquo;nın g&ouml;sterisiyle başlayan t&ouml;rene Başkan Savaş yanı sıra, MHP Hatay Milletvekili Şefik &Ccedil;irkin, Hatay Vali Yardımcısı Orhan Mardinli, sivil toplum kuruluşu temsilcileri, askeri erkan ve &ccedil;evre mahallelerden gelen &ccedil;ok sayıda vatandaş katılım g&ouml;sterdi.</p>\r\n<p>\r\n	T&ouml;renin a&ccedil;ılış konuşmasını yapan Muhtar G&uuml;ney, Bayrağın bir &uuml;lkenin bağımsızlığının simgesi olduğuna vurgu yaparak &ldquo;T&uuml;rk Bayrağı&rsquo;nın rengini şehitlerimizin kanından alması vatanımız i&ccedil;in &ouml;nemli bir kutsallık taşımaktadır. Bir &uuml;lkenin &ouml;zg&uuml;rl&uuml;ğ&uuml;n&uuml; ve bağımsızlığını temsil etmesi a&ccedil;ısından &ouml;nemlidir. Bağımsızlığı simgelediği i&ccedil;in g&ouml;klerde dalgalanır. Bayrak bir &uuml;lkenin onur ve şerefidir&rdquo; dedi.</p>\r\n<p>\r\n	<br />\r\n	&nbsp;&nbsp;&nbsp; Mahalle sakinleri tarafından sıcak ve samimi bir şekilde karşılanan Başkan Savaş konuşmasında ise, Yerel se&ccedil;imlerde her il&ccedil;enin y&uuml;ksek noktasını bayrak dikme s&ouml;z&uuml;n&uuml; hatırlatarak &ldquo;Bayrağımız bizim &ouml;zg&uuml;rl&uuml;ğ&uuml;m&uuml;z&uuml;, namusumuzu, ecdatlarımızı, şehitlerimizi, vatanımızı, bağımsızlığımızı ve her şeyimizi ifade eden bu Ay Yıldızlı bayrak &uuml;zerindeki rengi şehitlerimizden almıştır&rdquo; dedi.</p>\r\n<p>\r\n	<br />\r\n	&nbsp;&nbsp;&nbsp; Bayrağı buraya dikmeyi d&uuml;ş&uuml;nen Muhtar G&uuml;ney&rsquo;e ve destek veren t&uuml;m hemşehrilerine teşekk&uuml;r eden Başkan Savaş, &ldquo;Şu ger&ccedil;eği herkes bilmeli ki Hatay halkı bayrağına, namusuna, vatanına birlik ve beraberliğine de d&uuml;şk&uuml;n bir halktır. Ortadoğu&rsquo;daki yangına rağmen Hatay halkı birlik ve beraberliğinde en b&uuml;y&uuml;k etken olmuştur. Bunun en b&uuml;y&uuml;k sebeplerinden ilki ecdatlarımızdan almış olduğumuz birlik, beraberlik ve kardeşlik ruhu, ikincisi ise bayrağa olan sevgimizdir&rdquo; dedi.</p>\r\n','Hatay Büyükşehir,Belediye Başkanı,Lütfü Savaş,Necmettin Güney,MHP,Milletvekili','Hatay','2014-12-31 14:30:00','2014-12-31 14:30:00',1,0,0,0,1,1,1,1),
 (3,'2538bb2e-dec7-4eab-bafc-24a66f23d2ed','0001','hatayda-kacakcilik-operasyonlari-115933-fa68b0.jpg','Hatay\'da Kaçakçılık Operasyonları','Hatayda jandarma ekipleri tarafından 23-30 Aralık tarihleri arasında gerçekleştirilen aramalarda','<p>\r\n	Hatay İl Jandarma Komutanlığı sorumluluk b&ouml;lgesinde ka&ccedil;ak&ccedil;ılık ve organize su&ccedil;larla m&uuml;cadeleye y&ouml;nelik yapılan 71 &ccedil;alışmada; 7 bin 282 litre ka&ccedil;ak akaryakıt, 73 bin 930 paket ka&ccedil;ak sigara, 96 litre ka&ccedil;ak i&ccedil;ki, 11 adet k&uuml;&ccedil;&uuml;kbaş, 24 adet b&uuml;y&uuml;kbaş hayvan ele ge&ccedil;irildi.</p>\r\n<p>\r\n	Ele ge&ccedil;irilen akaryakıt ve sigara ile piyasa değeri toplamda 160 bin TL olan ka&ccedil;ak &uuml;r&uuml;n&uuml;n piyasaya s&uuml;r&uuml;lmesinin &ouml;n&uuml;ne ge&ccedil;ilirken g&ouml;zaltına alınan 55 kişi hakkında adli makamlar tarafından tahkikat başlatıldı.</p>\r\n<blockquote>\r\n	<p>\r\n		<strong>73 bin 930 paket sigara ve 7 bin 282 litre ka&ccedil;ak akaryakıt ele ge&ccedil;irildi.</strong></p>\r\n</blockquote>\r\n','Hatay,jandarma,sigara,organize,akaryakıt,piyasa','Hatay','2014-12-31 11:31:00','2014-12-31 12:09:02',1,0,0,0,1,1,1,1),
 (5,'2538bb2e-dec7-4eab-bafc-24a66f23d2ed','0006','iskenderun-spor-hazirlaniyor8207-140339-d07867.jpg','HATAY KÖRFEZ İSKENDERUN SPOR HAZIRLANIYOR&#8207;','Spor Toto 2. Lig Beyaz Grubunda mücadele eden Körfez İskenderun Spor, 11 Ocak 2015 tarihinde başlayacak olan lig öncesinde çalışmalarını','<p>\r\n	teknik Direkt&ouml;r Ramazan Silin nezaretinde s&uuml;rd&uuml;r&uuml;yor. İskenderun 5 Temmuz stadında sabah ve &ouml;ğleden sonra olmak &uuml;zere g&uuml;nde &ccedil;ift idman yaparak lige hazırlanan Turuncu Mavili takımda t&uuml;m futbolcuların hırslı olmaları teknik heyeti sevindiriyor.</p>\r\n<p>\r\n	K&ouml;rfez İskenderun Spor Teknik Direkt&ouml;r&uuml; Ramazan Silin, &quot;Sabah ve &ouml;ğleden sonra olmak &uuml;zere g&uuml;nde &ccedil;ift idman yaparak ikinci yarıya hazırlanmaktayız. Ligin ilk haftasında kendi sahamızda yapacağımız Karag&uuml;mr&uuml;k Spor karşılaşmasına kadar iki &ouml;zel m&uuml;sabaka oynayacağız. Bu karşılaşmalarda kadromuzda bulunan t&uuml;m futbolcularımıza forma giydireceğiz.</p>\r\n<p>\r\n	Yılbaşı nedeniyle futbolcularımıza 31 Aralık &Ccedil;arşamba g&uuml;n&uuml; 1 g&uuml;n izin vereceğiz. Daha sonra da &ccedil;alışmalarımıza İskenderun 5 Temmuz stadında tekrar devam edeceğiz&quot; dedi.</p>\r\n<p>\r\n	Ligin ikinci yarısının başlamasına az bir zaman kala &ccedil;alışmalarını İskenderun 5 Temmuz stadında s&uuml;rd&uuml;ren K&ouml;rfez İskenderun Spor&#39;da takım kaptanı Mehmet &Ouml;ncan ile Murat Kurt, taraftarlara mesaj g&ouml;ndererek, &quot;Ligin ilk yarısında aldığımız talihsiz sonu&ccedil;ların ardından puan cetvelinde istenilen yerde değiliz. Bundan da b&uuml;y&uuml;k &uuml;z&uuml;nt&uuml; duymaktayız.</p>\r\n<p>\r\n	Arkadaşlarımızla bir araya gelerek ikinci yarıda takımımızı laik olduğu yerlere taşımak i&ccedil;in t&uuml;m g&uuml;c&uuml;m&uuml;z&uuml; ortaya koyacağız. Ge&ccedil;mişte olduğu gibi bundan sonra da cefakar taraftarlarımızdan b&uuml;y&uuml;k destek beklemekteyiz&quot; dedi</p>\r\n','Körfez İskenderun Spor,Ramazan Silin,5 Temmuz','Hatay','2014-12-31 14:37:00','2014-12-31 14:37:00',1,0,0,0,1,1,1,1);
/*!40000 ALTER TABLE `haber` ENABLE KEYS */;


--
-- Definition of table `hesap`
--

DROP TABLE IF EXISTS `hesap`;
CREATE TABLE `hesap` (
  `id` varchar(36) NOT NULL,
  `ip` varchar(15) DEFAULT NULL,
  `adi` varchar(18) CHARACTER SET utf8 COLLATE utf8_turkish_ci NOT NULL,
  `soyadi` varchar(15) DEFAULT NULL,
  `mail` varchar(60) NOT NULL,
  `sifre` varchar(34) NOT NULL,
  `roller` varchar(40) NOT NULL DEFAULT 'U',
  `onaykodu` varchar(5) DEFAULT NULL,
  `dogumtarihi` datetime DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `cinsiyet` smallint(6) DEFAULT NULL,
  `tipi` smallint(6) DEFAULT NULL,
  `yorum` tinyint(1) DEFAULT NULL,
  `abonelik` tinyint(1) DEFAULT NULL,
  `aktivasyon` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `unique_hesapmail` (`mail`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `hesap`
--

/*!40000 ALTER TABLE `hesap` DISABLE KEYS */;
INSERT INTO `hesap` (`id`,`ip`,`adi`,`soyadi`,`mail`,`sifre`,`roller`,`onaykodu`,`dogumtarihi`,`kayittarihi`,`cinsiyet`,`tipi`,`yorum`,`abonelik`,`aktivasyon`,`aktif`) VALUES 
 ('2538bb2e-dec7-4eab-bafc-24a66f23d2ed','::1','Sistem','Yöneticisi','admin@baymyo.com','369B92F948CA9407A39157B0BCFDE0BD','P,A,J,T,H,M,R,I,Q,F,S,V,G,Y,O,E,U','72862','1985-10-01 00:00:00','2014-12-24 14:10:00',1,1,1,1,1,1);
/*!40000 ALTER TABLE `hesap` ENABLE KEYS */;


--
-- Definition of table `kategori`
--

DROP TABLE IF EXISTS `kategori`;
CREATE TABLE `kategori` (
  `id` varchar(25) NOT NULL,
  `parentid` varchar(25) DEFAULT NULL,
  `modulid` varchar(15) DEFAULT NULL,
  `adi` varchar(35) DEFAULT NULL,
  `dil` varchar(8) DEFAULT NULL,
  `sira` smallint(6) DEFAULT NULL,
  `menu` smallint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  `renk` char(7) DEFAULT '#cf0a0a',
  `aciklama` varchar(150) DEFAULT NULL,
  `etiket` varchar(100) DEFAULT NULL,
  KEY `index_kategroiid` (`id`,`parentid`,`modulid`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `kategori`
--

/*!40000 ALTER TABLE `kategori` DISABLE KEYS */;
INSERT INTO `kategori` (`id`,`parentid`,`modulid`,`adi`,`dil`,`sira`,`menu`,`aktif`,`renk`,`aciklama`,`etiket`) VALUES 
 ('0008','0','egitim','Lisans','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0007','0','egitim','Ön Lisans','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0006','0','egitim','Anadolu Lisesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0005','0','egitim','Anadolu Meslek Lisesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0002','0','egitim','Orta Okul','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0001','0','egitim','İlk Okul','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0036','0','meslek','Gıda Hizmetleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0018','0','meslek','Seyahat Hizmetleri ','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0019','0','meslek','Kuru Temizlemecilik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0020','0','meslek','Kuyumcu','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0021','0','meslek','End. Modelleme','tr-TR',0,0,0,'#cf0a0a',NULL,NULL),
 ('0022','0','meslek','Makine Ressamı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0023','0','meslek','Makine İmalatı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0024','0','meslek','Mermer İşleme','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0025','0','meslek','Makine Bakım','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0026','0','meslek','Anahtarcılık','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0027','0','meslek','Matbaa','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0028','0','meslek','Metal Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0029','0','meslek','Metalurji Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0030','0','meslek','Motorlu Araç Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0031','0','meslek','Pazarlama ve Perakende','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0032','0','meslek','Sigortacılık','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0033','0','meslek','Plastik Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0004','0','egitim','Düz Lise','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0003','0','egitim','Meslek Lisesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0035','0','meslek','Tekstil Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0034','0','meslek','Reklamcı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0017','0','meslek','Kimya Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0016','0','meslek','Kağıt Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0015','0','meslek','İnşaat Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0014','0','meslek','Güzellik ve Saç Bakımı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0013','0','meslek','Gıda Teknolojisi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0012','0','meslek','Giyim Üretimi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0011','0','meslek','Deniz Araçları Yapımı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0010','0','meslek','Fotoğrafçılık','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0009','0','meslek','El Sanatları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0008','0','meslek','Elektrik Elektronik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0007','0','meslek','Döşemecilik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0006','0','meslek','Sistem Uzmanı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0005','0','meslek','Yazılım Uzmanı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0004','0','meslek','Bilişim Teknolojileri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0003','0','meslek','Bahçecilik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0002','0','meslek','Ayakkabı ve Saraciye','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0001','0','meslek','Ahşap İşleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0009','0','egitim','Yüksek Lisans','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0037','0','meslek','Köşe Yazarı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0011','0','haber','Yerel','tr-TR',0,1,0,'#cf0a0a',NULL,NULL),
 ('0001','0','makale','Gazete Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0010','0','haber','Bilim Teknoloji','tr-TR',9,1,1,'#EF6C00','Teknolojinin en son gelişmeleri.','teknoloji,yazılım,software,program,baymyo'),
 ('0009','0','haber','Magazin','tr-TR',6,1,1,'#F50057','Magazinin kalbi ve jet sosyetelerin son haberleri.','magazin,ünlü,paparazi,sosyete'),
 ('0008','0','haber','Spor','tr-TR',7,1,1,'#1B5E20','İskenderun ve Hatay spor hakkında anlık haberler.','iskenderun,spor,hatay,haber'),
 ('0007','0','haber','Ekonomi','tr-TR',4,1,1,'#01579B','Finansal durum ve Piyasalardan anında haberdar olun.','borsa,finans,kredi,sektör'),
 ('0006','0','haber','Yaşam','tr-TR',5,1,1,'#FF5252','Yaşam ve Hayata dair ne varsa anlık olarak burada.','yaşam,haber,gazete,haberler'),
 ('0005','0','haber','Dünya','tr-TR',5,1,1,'#1A237E','Gurbetteki hemşerilerimizi ilgilendirecek tüm konular.','dünya,haberler,almanya'),
 ('0004','0','haber','Politika','tr-TR',3,1,1,'#3E2723','Meslisin gündemini ilk siz öğrenin.','politika,akp,mhp,chp'),
 ('0003','0','haber','Kültür Sanat','tr-TR',12,1,1,'#827717','Sinema, Tiyatro ve kültürel olaylar.','sinema,tiyatro,belgesel,iskenderun'),
 ('0002','0','haber','Sağlık','tr-TR',6,1,1,'#00BFA5','Türkiyenin sağlık gündeminden anlık haberler','sağlık,ebola,virüs'),
 ('0001','0','haber','Gündem','tr-TR',0,1,1,'#cf0a0a','Türkiye ve memelekt gündeminin şehrimize yansımalarından anında haberdar olun.','gündem,sondakika,haber'),
 ('0001','0','galeri','Foto Haber','tr-TR',0,1,1,'#cf0a0a','Gündemi fotoğrafların dilinden anlayın.','gündem,haber,fotoğraf'),
 ('0002','0','galeri','Foto Analiz','tr-TR',0,1,1,'#ef6c00','Fotoğrafları yorumluyoruz ve gündem olan konulara görsel açıdan bakıyoruz.','fotoğraf,analiz,haber,sondakika'),
 ('0003','0','galeri','Yaşam','tr-TR',2,1,1,'#ff5252','Yaşama dair fotoğraflar sağlık ve huzurun simgeleri.','yaşam,sağlık,hayat,moda,tarz,bu tarz benim'),
 ('0004','0','galeri','Eğlence','tr-TR',3,1,1,'#9575cd','Fotoğraflar ile eğlenceli dakikalar','eğlence,komik,koca kafalar,komedi,karikatür'),
 ('0005','0','galeri','Dünya','tr-TR',1,1,1,'#304ffe','Dünyanın gündemini belirleyen fotoğrafların analizleri.','dünya,sondakika,haber,gazete'),
 ('0006','0','galeri','Magazin','tr-TR',5,1,1,'#e91e63','Magazinin gündemine oturan fotoğraflar için tıklayın.','magazin,iskenderun,sondakika'),
 ('0007','0','galeri','Eğitim','tr-TR',2,1,1,'#757575','Eğitim ve üniversite gündemini fotoğraflar ile analiz edin.','eğitim,meb,mili eğitim'),
 ('0008','0','galeri','Tekonoloji','tr-TR',9,1,1,'#ffab00','Kültürel miras ve belgesellere konu olan fotoğraflar.','sanat,müze,kültür,sanat haberleri'),
 ('0001','0','firma','Bilim Teknoloji','tr-TR',0,1,1,'#cf0a0a','İskenderun daki tüm teknoloji ve bilişim firmaları.','iskenderun bilim,iskenderun tekno park'),
 ('0001','0','seriilan','Otomobil','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0003','0','seriilan','Alışveriş','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0006','0','seriilan','İş Makineleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0011','0','seriilan','El İşi & Sanat','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0007','0','seriilan','Hayvanlar Alemi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0008','0','seriilan','Hizmet','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0009','0','seriilan','Beyaz Eşya','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0010','0','seriilan','Elektronik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0014','0','seriilan','Medikal Ürünler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0013','0','seriilan','Spor Ürünleri','tr-TR',0,1,1,'#cf0a0a','Spor yapmak için gerekli ikinci el yada sıfır ürünler.','halter,futbol topu,basketbol topu,tenis'),
 ('0016','0','seriilan','Motosiklet','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0017','0','seriilan','Arazi, SUV','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0018','0','seriilan','Kiralık Araçlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0019','0','seriilan','Deniz Araçları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0020','0','seriilan','Yedek Parça','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0021','0','seriilan','Konut','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0022','0','seriilan','İş Yeri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0023','0','seriilan','Arsa','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0024','0','seriilan','Bina','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0025','0','seriilan','Devremülk','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0001','0','video','Web TV','tr-TR',12,1,1,'#e65100','Dizilerin fragmanlarını ve fenomen sahnelerini izlemek için tıklayın.','dizi,paramparça,kurtlarvadisi,kocamın ailesi'),
 ('0002','0','video','Müzik','tr-TR',5,1,1,'#34495e','Müzik severlerin ortak noktası ilk siz dinleyin.','müzik,aydilge,halil sezai,mp3,mp4'),
 ('0003','0','video','Yaşam','tr-TR',1,1,1,'#ff5252','Hayatımızı kolaylaştıran ve güzelleştiren anların videoları.','yaşam,sağlık,hayat,moda,tarz,bu tarz benim'),
 ('0004','0','video','Dünya','tr-TR',2,1,1,'#304ffe','Dünyada olan biteni ve haberleri izlemek için tıklayın.','dünya,haber,web tv'),
 ('0005','0','video','Haber','tr-TR',0,1,1,'#cf0a0a','Gündem belirleyen konuları izlemek için tıklayın.','gündem,haber,web tv'),
 ('0006','0','video','Magazin','tr-TR',3,1,1,'#ff4081','Magazin ve en son paparazi haberleri için tıklayın.','magazin,iskenderun,haber,sondakika'),
 ('0007','0','video','Sağlık','tr-TR',6,1,1,'#00bcd4','Sağlık ve uzman doktorların videoları için tıklayın.','sağlık,uzman,doktor'),
 ('0008','0','video','Spor','tr-TR',7,1,1,'#2e7d32','Maçların unutulmaz anları ve tekrarları için tıklayın.','maç,futbol,basketbol,voleybol,basebol,golf'),
 ('0009','0','video','Ekonomi','tr-TR',1,1,1,'#0277bd','Para piyasaları ve borsa canlı yayınları.','borsa,piyasalar,doviz,dolar,altın,euro'),
 ('0002','0','firma','Dövmeciler','tr-TR',0,1,1,'#cf0a0a','Dövme meraklıları en iyi dövme ve tatto firmaları bu rehberde.','iskenderun tatto,hatay dövme,hatay tatto'),
 ('0003','0','firma','Ağaç Ürünleri','tr-TR',0,1,1,'#cf0a0a','Ağaç ve Ahşap firmaları sizde burada yerinizi alabilirsiniz.','ağaç,ahşap,iskenderun,istanbul,ankara'),
 ('0004','0','firma','Bahçe ve Çiçek','tr-TR',0,1,1,'#cf0a0a','Tıklayın tüm bahçe ve çiçek firmalarını görüntüleyin.','bahçe,çiçek,peyzaş'),
 ('0005','0','firma','Çevre ve Su','tr-TR',0,1,1,'#cf0a0a','Çevre analizi ve su raporları gibi belgele sağlayıcı firmalar.','hatsu,isg,çevre,çed'),
 ('0006','0','firma','İddaa ve Ganyan Bayileri','tr-TR',0,1,1,'#cf0a0a','Bahse varmısınız size en yakın idda ve loto bayilerini biz biliyoruz.','iskenderun tekel,hatay iddia,iskenderun loto,iskenderun toto'),
 ('0007','0','firma','Elektrik ve Elektronik','tr-TR',0,1,1,'#cf0a0a','Elektronik cihazınız mı bozuldu tamirci mi arıyorsunuz tıklayın firmaları görüntüleyin.','elektrikçi,elektronik,tamir,priz,ampul'),
 ('0008','0','firma','Alışveriş ve Perakende','tr-TR',0,1,1,'#cf0a0a','Alışveriş ve Perakende ürün alabileceğiniz firmaların listesi.','iskenderun perakende,hatay alışveriş'),
 ('0009','0','firma','Basın ve Yayın','tr-TR',0,1,1,'#cf0a0a','Tüm yerel ve bölgesel basın yayın kuruluşları.','basın,yayın,gazete,haber'),
 ('0010','0','firma','Boya İmalat ve Sanayi','tr-TR',0,1,1,'#cf0a0a','Boya imalat fabrikaları ve perakende satış bayileri.','hatay boya,iskenderun boyacı,iskenderun dekor'),
 ('0011','0','firma','Dekorasyon','tr-TR',0,1,1,'#cf0a0a','Dekorasyon işleriniz için firma yada ustamı arıyorsunuz tıklayın.','hatay tamir ve bakım,iskenderun dekorasyon,kırıkhan ustaları,inşaat'),
 ('0012','0','firma','Dershaneler ve Kurslar','tr-TR',0,1,1,'#cf0a0a','Hatay,İskenderun ve Çevresindeki tüm dershanelerin iletişim bilgileri.','hatay dershane,iskenderun etüt,eğitim'),
 ('0013','0','firma','İnşaat Firmaları ve Müteahitler','tr-TR',0,1,1,'#cf0a0a','Hatay, İskenderun ve Civarındaki tüm inşaat malzemesi satan firmalar.','hatay inşaat,iskenderun build,iskenderun tuğla,hatay kerpiç'),
 ('0014','0','firma','Ambalaj Sanayi','tr-TR',0,1,1,'#cf0a0a','Hatay, İskenderun ve civarındaki tüm ambalaj satışı yapan firmalar.','iskenderun kağıt bardak,hatay ambalaj,iskenderun poşet'),
 ('0015','0','firma','Bilgisayar ve Yazılım','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0016','0','firma','Denizcilik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0002','0','makale','Spor Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0003','0','makale','Magazin Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0004','0','makale','İstanbul Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0005','0','makale','Ctesi-Pazar Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0006','0','makale','Ege Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0007','0','makale','Ankara Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0008','0','makale','Ekonomi Köşesi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0038','0','meslek','G. Yayın Kordinatörü ','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0039','0','meslek','Sorumlu Müdür','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0040','0','meslek','G. Yayın Yönetmeni ','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0041','0','meslek','G.Yayın Md. Yardımcısı ','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0042','0','meslek','Haber Koordinatörü','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0012','0','haber','Medya','tr-TR',5,1,1,'#263238','Medyatik konular ve anlık haberler.','medya,gazete,güncel,durum'),
 ('0013','0','haber','Eğitim','tr-TR',10,1,1,'#5C6BC0','Eğitimdeki yeniliklerden haberiniz olsun.','eğitim,meb,iskenderun'),
 ('0017','0','firma','Anahtar ve Kilit','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0018','0','firma','Avukatlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0019','0','firma','Akaryakıt Bayileri','tr-TR',0,1,1,'#cf0a0a','Hatay,İskenderun,Kırıkhan ve Dörtyol civarındaki tüm benzin istasyonları.','iskenderun benzin,iskenderun mazot,hatay mazot,dörtyol ucuz benzin'),
 ('0020','0','firma','Demirçelik Soğuk Çekim İmalatı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0021','0','firma','Asansör Satış, Bakım ve Onarın','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0022','0','firma','Balıkçılar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0023','0','firma','Bar ve Diskolar','tr-TR',0,1,1,'#cf0a0a','Hatay, İskenderun ve Arsuz civarındaki tüm eğlence mekanları için tıklayın.','iskenderun bar,hatay disko,arsuz'),
 ('0024','0','firma','Boru İmalat ve Sanayi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0025','0','firma','Berber ve Erkek Kuaförleri','tr-TR',0,1,1,'#cf0a0a','Hatay,İskenderun ve Civarındaki tüm berber ve erkek kuaförleri.','iskenderun berber,hatay kuaför,erkek kuaförü,berber'),
 ('0026','0','firma','Dalgıç Hizmetleri','tr-TR',0,1,1,'#cf0a0a','İskenderun köfezinde dalgıçlık hizmetleri verebilen tüm firmalar burada.','iskenderun körfez,dalgıç,samandağı,gemi'),
 ('0027','0','firma','Gemi Malzemeleri ve Kumanyacılığı','tr-TR',0,1,1,'#cf0a0a','İskenderun körfezindeki tüm kumanya ve toptancıların irtibat bilgileri.','iskenderun kumanya,tedarik,gıda toptancı'),
 ('0028','0','firma','Dalma ve Sulama Sistemleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0029','0','firma','Eczneler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0030','0','firma','Emlakçılar','tr-TR',0,1,1,'#cf0a0a','Hatay ve genelindeki tüm emlakçıların bilgileri burada.','hatay emlak,iskenderun emlak,gayrimenkul,daire,konut'),
 ('0031','0','firma','Elektrik Malzemeleri Satış ve Bakım','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0032','0','firma','Elektrik Proje Mühendislik','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0033','0','firma','Av Malzemeleri ve Paylayıcılar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0034','0','firma','Anaokulları ve Kreşler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0035','0','firma','Baharatçılar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0036','0','firma','Bebe Giyim','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0037','0','firma','Bilgisayar Satış, Bakım ve Donanım','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0038','0','firma','Büro Mobilyaları ve İmalatı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0039','0','firma','Cam Çerçeve','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0040','0','firma','Cam Balkon Sistemleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0041','0','firma','Çeyiz Nakış İşleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0042','0','firma','Direksiyon Gelişim Merkezi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0043','0','firma','Hırdavatçılar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0044','0','firma','Güzellik Salonları ve Poliklinikler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0045','0','firma','Haşere ve Zirai İlaçlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0046','0','firma','Hediyelik Eşya Mağazaları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0047','0','firma','İç Dekorasyon','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0048','0','firma','İnşaat Market ve Yapı Malzemeleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0049','0','firma','İthalat ve İhracat Firmaları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0050','0','firma','Kargo Şirketleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0051','0','firma','Manavlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0052','0','firma','Oteller','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0053','0','firma','Oto Tamir ve Yetkili Servisler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0054','0','firma','Oto Yetkili Satıcıları ve Galeriler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0055','0','firma','Öğrenci Servisleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0056','0','firma','Özel Hastaneler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0057','0','firma','Pansiyonlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0058','0','firma','Özel Okullar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0059','0','firma','Pet Shop','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0060','0','firma','Playstation Salonları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0061','0','firma','İnternet Cafeler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0062','0','firma','Reklam Ajansları ve Promosyonlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0063','0','firma','Sigorta Acenteler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0064','0','firma','SRC ve Pisicoteknik Danışmanlığı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0065','0','firma','Tavukçuluk','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0066','0','firma','Turizm Acenteleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0067','0','firma','Zeka Geliştirme Merkezleri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0068','0','firma','Yetkili Servisler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0069','0','firma','Zirai İlaçlar ve Tarım Makinaları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0070','0','firma','Telekom Bayileri','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0071','0','firma','Sürücü Kursları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0072','0','firma','Spor Mağazaları ve Salonları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0073','0','firma','Organizasyon','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0074','0','firma','Odun Kömür Satışı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0075','0','firma','Nakliyat Şirketleri ve Ambarlar','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0076','0','firma','Muhasebeciler ve Müşavirler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0077','0','firma','Mobilya İmalat ve Satış','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0078','0','firma','Makine ve Motor Yağları','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0088','0','firma','Matbaalar','tr-TR',0,1,1,'#cf0a0a','Aradağınız tüm ofset baskı firmaları burada.','ofset,matba,fatura,belge'),
 ('0080','0','firma','Medikal ve Ortopedi','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0081','0','firma','Mefruşat ve Ev Tekstili','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0082','0','firma','2. El Eşya Alım ve Satımı','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0083','0','firma','Kuyumcular ve Gümüşçüler','tr-TR',0,1,1,'#cf0a0a',NULL,NULL),
 ('0014','0','haber','Yorumluyorum','tr-TR',0,0,1,'#006064','Adnan Tink farkıyla haberler.','adnan tink,hür haber,yorumluyorum'),
 ('0015','0','haber','İskenderun','tr-TR',14,0,1,'#2962FF','İskenderun haberleri ve gazeteleri anında haberdar olmak için tıkla.','iskenderun,haber,sondakika,gazete'),
 ('0016','0','haber','Hatay','tr-TR',15,0,1,'#9575CD','Hatay dan sondakika haberleri ile sizlerleyiz.','hatay,antakya'),
 ('0017','0','haber','Arsuz','tr-TR',16,0,1,'#EF6C00','Arsuz yaz tatilinin en nadide mekanlarından biri son gelişmelerden haberiniz olsun.','aruz,tatil,haber,sondakika'),
 ('0018','0','haber','Kırıkhan','tr-TR',16,0,1,'#795548','Kırıkhandaki en son olaylardan haberiniz olsun.','kırıkhan,haber,enson gelişmeler'),
 ('0019','0','haber','Reyhanlı','tr-TR',17,2,1,'#A1887F','Reyhanlı ile ilgili son gelişmeler.','reyhanlı,sınır,suriye'),
 ('0020','0','haber','Payas','tr-TR',18,0,1,'#757575','Demirçelik ve sanayi şehri hakkındaki anlık gelişmeler.','payas,demirçelik,isdemir,osb'),
 ('0021','0','haber','Dörtyol','tr-TR',19,0,1,'#78909C','Dörtyol portakalın anavatanından anlık gelişmeler.','dörtyol,ilk kurşun,gazete,haber'),
 ('0022','0','haber','Belen','tr-TR',20,0,1,'#546E7A','Belen için anlık haberler.','belen,haber,gazete'),
 ('0084','0','firma','Oto Kuaför ve Oto Kiralama','tr-TR',0,1,1,'#cf0a0a','Otomobil kuaförleri ve en uygun oto kiralama firmaları.','otomobil,oto kuaför,oto kiralama,oto galeri'),
 ('0010','0','video','Teknoloji','tr-TR',2,0,1,'#959582','Gelişen teknoloji ve yeni ürünler hakkında bilgi edinmek için tıklayın.','teknoloji,akıllı telefon,televizyon,led'),
 ('0011','0','video','Eğlence','tr-TR',5,0,1,'#ffc400','Eğlenceli içerikler ve komik anlarımızla gülelim eğlenelim.','eğlence,komik,koca kafalar,komedi,karikatür'),
 ('0009','0','galeri','Ünlüler','tr-TR',6,1,1,'#a1887f','Ünlülerin yaşam tarzlarını merake diyorsanız buyurun beraber bakalım.','ünlü,giyim,nerede,gezi'),
 ('0010','0','galeri','Spor','tr-TR',4,1,1,'#558b2f','En güzel futbol, basketbol ve diğer spor dallarının karelerini görmek için tıkla.','maç,futbol,basketbol,voleybol,basebol,golf'),
 ('0085','0','firma','Çiçekçiler','tr-TR',0,0,1,'#cf0a0a','Kime nereye hangi şehire çiçek göndermek istiyorsanız tüm çiçekçiler burada.','çiçek,çiçekçiler,interflora'),
 ('0026','0','seriilan','Akıllı Telefonlar','tr-TR',0,0,1,'#cf0a0a','İkinci el tep tüm markaların cep telefonlarını çok ucuza elden almanın yolu.','lg,samsung,note serisi,iphone'),
 ('0086','0','firma','Kebap, Döner ve Restaurantlar','tr-TR',0,1,1,'#CF0A0A','Karnınız çok mu acıktı pratik bir şey mi yemek istiyorsunuz o zaman bu firmalara bir göz atın.','döner,kebab,çorba,yemek,simit'),
 ('0087','0','firma','Gıda Toptancıları','tr-TR',0,0,1,'#CF0A0A','Toptan ve Perakende satış yapabilen firmaları.','toptancı,perakende,market,gıda'),
 ('0089','0','firma','Marketler ve Bakkalar','tr-TR',0,1,1,'#cf0a0a','Süper Market ve Bakallar aradığınız tüm perakendeciler.','perakende,market'),
 ('0090','0','firma','Pizzacılar ve Fast Food','tr-TR',0,1,1,'#cf0a0a','Pizza, Hamburger ve tüm fast food firmalarına kolay ulaşım bilgileri.','iskenderun pizza,hamburger,iskenderun fast food'),
 ('0091','0','firma','Beyaz Eşyalar','tr-TR',0,1,1,'','',''),
 ('0092','0','firma','Bujiteri Takı Çeşitleri','tr-TR',0,1,1,'','',''),
 ('0093','0','firma','Branda Ve Tente Çeşitleri','tr-TR',0,1,1,'','',''),
 ('0094','0','firma','Çatı Malzemeleri','tr-TR',0,1,1,'','',''),
 ('0095','0','firma','Kopya Copy Center Kırtasiye','tr-TR',0,1,1,'','',''),
 ('0096','0','firma','Dayanıklı Tüketim Malları','tr-TR',0,1,1,'','',''),
 ('0097','0','firma','Öğrenci Yurtları','tr-TR',0,1,1,'','Özel ve Devlet Öğrenci Yurtları','öğrenci yurtları,kyk,devlet yurtları');
/*!40000 ALTER TABLE `kategori` ENABLE KEYS */;


--
-- Definition of table `makale`
--

DROP TABLE IF EXISTS `makale`;
CREATE TABLE `makale` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `kategoriid` varchar(25) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `baslik` varchar(75) DEFAULT NULL,
  `ozet` varchar(150) DEFAULT NULL,
  `icerik` longtext CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `etiket` varchar(100) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `uye` tinyint(1) DEFAULT NULL,
  `yorum` tinyint(1) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_makaleid` (`kategoriid`),
  FULLTEXT KEY `index_makaletext` (`baslik`,`etiket`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `makale`
--

/*!40000 ALTER TABLE `makale` DISABLE KEYS */;
/*!40000 ALTER TABLE `makale` ENABLE KEYS */;


--
-- Definition of table `manset`
--

DROP TABLE IF EXISTS `manset`;
CREATE TABLE `manset` (
  `id` varchar(36) NOT NULL,
  `modulid` varchar(15) DEFAULT NULL,
  `icerikid` varchar(40) DEFAULT NULL,
  `resimkucuk` varchar(50) DEFAULT NULL,
  `resimbuyuk` varchar(50) DEFAULT NULL,
  `baslik` varchar(75) DEFAULT NULL,
  `aciklama` varchar(150) DEFAULT NULL,
  `dil` varchar(8) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `yerlesim` smallint(6) DEFAULT NULL,
  `baslikgoster` tinyint(1) unsigned NOT NULL DEFAULT '1',
  `anasayfa` tinyint(1) unsigned NOT NULL DEFAULT '0',
  `sira` tinyint(1) unsigned NOT NULL DEFAULT '15',
  `aktif` tinyint(1) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `index_mansetid` (`modulid`,`icerikid`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `manset`
--

/*!40000 ALTER TABLE `manset` DISABLE KEYS */;
INSERT INTO `manset` (`id`,`modulid`,`icerikid`,`resimkucuk`,`resimbuyuk`,`baslik`,`aciklama`,`dil`,`kayittarihi`,`guncellemetarihi`,`yerlesim`,`baslikgoster`,`anasayfa`,`sira`,`aktif`) VALUES 
 ('dffbd271-22f9-47ad-9a7e-ae0b5066c911','haber','3','a-kacakcilik-operasyonlari-kucuk-125306-ea4e23.jpg','a-kacakcilik-operasyonlari-buyuk-113534-d7b511.jpg','Hatay\'da Kaçakçılık Operasyonları','Hatayda jandarma ekipleri tarafından 23-30 Aralık tarihleri arasında gerçekleştirilen aramalarda','','2014-12-31 11:31:00','2014-12-31 12:08:05',3,1,1,0,1);
/*!40000 ALTER TABLE `manset` ENABLE KEYS */;


--
-- Definition of table `mesaj`
--

DROP TABLE IF EXISTS `mesaj`;
CREATE TABLE `mesaj` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `ip` varchar(15) DEFAULT NULL,
  `adi` varchar(35) DEFAULT NULL,
  `mail` varchar(60) DEFAULT NULL,
  `telefon` varchar(16) DEFAULT NULL,
  `konu` varchar(50) DEFAULT NULL,
  `icerik` varchar(1000) DEFAULT NULL,
  `yanit` varchar(1500) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `durum` smallint(6) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_mesajid` (`hesapid`,`mail`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `mesaj`
--

/*!40000 ALTER TABLE `mesaj` DISABLE KEYS */;
/*!40000 ALTER TABLE `mesaj` ENABLE KEYS */;


--
-- Definition of table `profil`
--

DROP TABLE IF EXISTS `profil`;
CREATE TABLE `profil` (
  `id` varchar(36) NOT NULL,
  `url` varchar(35) NOT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `adi` varchar(50) DEFAULT NULL,
  `mail` varchar(60) DEFAULT NULL,
  `web` varchar(60) DEFAULT NULL,
  `telefon` varchar(16) DEFAULT NULL,
  `gsm` varchar(16) DEFAULT NULL,
  `meslek` varchar(4) DEFAULT NULL,
  `egitim` varchar(4) DEFAULT NULL,
  `sehir` varchar(20) DEFAULT NULL,
  `hakkimda` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_profilid` (`url`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `profil`
--

/*!40000 ALTER TABLE `profil` DISABLE KEYS */;
INSERT INTO `profil` (`id`,`url`,`resimurl`,`adi`,`mail`,`web`,`telefon`,`gsm`,`meslek`,`egitim`,`sehir`,`hakkimda`) VALUES 
 ('2538bb2e-dec7-4eab-bafc-24a66f23d2ed','sistemyoneticisi','sistem-yoneticisi-145909-31805f.png','Portal Yönetimi','info@baymyo.com','www.baymyo.com','','5448954747','0004','0008','Hatay','Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500\'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960\'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMak');
/*!40000 ALTER TABLE `profil` ENABLE KEYS */;


--
-- Definition of table `reklam`
--

DROP TABLE IF EXISTS `reklam`;
CREATE TABLE `reklam` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bannername` varchar(50) DEFAULT NULL,
  `imageurl` longtext,
  `navigateurl` varchar(200) DEFAULT NULL,
  `alternatetext` varchar(200) DEFAULT NULL,
  `keyword` varchar(200) DEFAULT NULL,
  `adsenseclient` varchar(30) DEFAULT NULL,
  `adsenseslot` varchar(30) DEFAULT NULL,
  `impressions` int(11) NOT NULL,
  `width` int(11) DEFAULT NULL,
  `height` int(11) DEFAULT NULL,
  `orders` tinyint(3) unsigned DEFAULT '1',
  `isactive` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `reklam`
--

/*!40000 ALTER TABLE `reklam` DISABLE KEYS */;
INSERT INTO `reklam` (`id`,`bannername`,`imageurl`,`navigateurl`,`alternatetext`,`keyword`,`adsenseclient`,`adsenseslot`,`impressions`,`width`,`height`,`orders`,`isactive`) VALUES 
 (1,'YORUM ÜSTÜ REKLAMLARI','<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>\r\n<!-- 8GUN YORUM USTU -->\r\n<ins class=\"adsbygoogle\"\r\n     style=\"display:inline-block;width:728px;height:90px\"\r\n     data-ad-client=\"ca-pub-3840974574011310\"\r\n     data-ad-slot=\"4452467643\"></ins>\r\n<script>\r\n(adsbygoogle = window.adsbygoogle || []).push({});\r\n</script>','','','yorum,haber','','',0,728,90,6,1);
/*!40000 ALTER TABLE `reklam` ENABLE KEYS */;


--
-- Definition of table `resmiilan`
--

DROP TABLE IF EXISTS `resmiilan`;
CREATE TABLE `resmiilan` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `baslik` varchar(100) DEFAULT NULL,
  `icerik` longtext CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `sehir` varchar(20) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `resmiilan`
--

/*!40000 ALTER TABLE `resmiilan` DISABLE KEYS */;
/*!40000 ALTER TABLE `resmiilan` ENABLE KEYS */;


--
-- Definition of table `sayfa`
--

DROP TABLE IF EXISTS `sayfa`;
CREATE TABLE `sayfa` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `baslik` varchar(50) DEFAULT NULL,
  `icerik` longtext CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `dil` varchar(8) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `yerlesim` smallint(6) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sayfa`
--

/*!40000 ALTER TABLE `sayfa` DISABLE KEYS */;
/*!40000 ALTER TABLE `sayfa` ENABLE KEYS */;


--
-- Definition of table `sehir`
--

DROP TABLE IF EXISTS `sehir`;
CREATE TABLE `sehir` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `adi` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_sehirtext` (`adi`)
) ENGINE=MyISAM AUTO_INCREMENT=82 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sehir`
--

/*!40000 ALTER TABLE `sehir` DISABLE KEYS */;
INSERT INTO `sehir` (`id`,`adi`) VALUES 
 (1,'Adana'),
 (2,'Adıyaman'),
 (3,'Afyon'),
 (4,'Ağrı'),
 (5,'Amasya'),
 (6,'Ankara'),
 (7,'Antalya'),
 (8,'Artvin'),
 (9,'Aydın'),
 (10,'Balıkesir'),
 (11,'Bilecik'),
 (12,'Bingöl'),
 (13,'Bitlis'),
 (14,'Bolu'),
 (15,'Burdur'),
 (16,'Bursa'),
 (17,'Çanakkale'),
 (18,'Çankırı'),
 (19,'Çorum'),
 (20,'Denizli'),
 (21,'Diyarbakır'),
 (22,'Edirne'),
 (23,'Elazığ'),
 (24,'Erzincan'),
 (25,'Erzurum'),
 (26,'Eskişehir'),
 (27,'Gaziantep'),
 (28,'Giresun'),
 (29,'Gümüşhane'),
 (30,'Hakkari'),
 (31,'Hatay'),
 (32,'Isparta'),
 (33,'İçel'),
 (34,'İstanbul'),
 (35,'İzmir'),
 (36,'Kars'),
 (37,'Kastamonu'),
 (38,'Kayseri'),
 (39,'Kırklareli'),
 (40,'Kırşehir'),
 (41,'Kocaeli'),
 (42,'Konya'),
 (43,'Kütahya'),
 (44,'Malatya'),
 (45,'Manisa'),
 (46,'Kahramanmaraş'),
 (47,'Mardin'),
 (48,'Muğla'),
 (49,'Muş'),
 (50,'Nevşehir'),
 (51,'Niğde'),
 (52,'Ordu'),
 (53,'Rize'),
 (54,'Sakarya'),
 (55,'Samsun'),
 (56,'Siirt'),
 (57,'Sinop'),
 (58,'Sivas'),
 (59,'Tekirdağ'),
 (60,'Tokat'),
 (61,'Trabzon'),
 (62,'Tunceli'),
 (63,'Şanlıurfa'),
 (64,'Uşak'),
 (65,'Van'),
 (66,'Yozgat'),
 (67,'Zonguldak'),
 (68,'Aksaray'),
 (69,'Bayburt'),
 (70,'Karaman'),
 (71,'Kırıkkale'),
 (72,'Batman'),
 (73,'Şırnak'),
 (74,'Bartın'),
 (75,'Ardahan'),
 (76,'Iğdır'),
 (77,'Yalova'),
 (78,'Karabük'),
 (79,'Kilis'),
 (80,'Osmaniye'),
 (81,'Düzce');
/*!40000 ALTER TABLE `sehir` ENABLE KEYS */;


--
-- Definition of table `seriilan`
--

DROP TABLE IF EXISTS `seriilan`;
CREATE TABLE `seriilan` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) NOT NULL,
  `kategoriid` varchar(8) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `baslik` varchar(75) DEFAULT NULL,
  `detay` varchar(1000) DEFAULT NULL,
  `semt` varchar(30) DEFAULT NULL,
  `sehir` varchar(20) DEFAULT NULL,
  `telefon` varchar(16) DEFAULT NULL,
  `fiyat` float DEFAULT NULL,
  `kimden` smallint(6) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_seriilanid` (`hesapid`,`kategoriid`),
  FULLTEXT KEY `index_seriilantext` (`baslik`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `seriilan`
--

/*!40000 ALTER TABLE `seriilan` DISABLE KEYS */;
/*!40000 ALTER TABLE `seriilan` ENABLE KEYS */;


--
-- Definition of table `video`
--

DROP TABLE IF EXISTS `video`;
CREATE TABLE `video` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `hesapid` varchar(36) DEFAULT NULL,
  `kategoriid` varchar(8) DEFAULT NULL,
  `resimurl` varchar(50) DEFAULT NULL,
  `baslik` varchar(75) DEFAULT NULL,
  `embed` varchar(750) DEFAULT NULL,
  `etiket` varchar(100) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `guncellemetarihi` datetime DEFAULT NULL,
  `gosterimsayi` tinyint(1) DEFAULT NULL,
  `uye` tinyint(1) DEFAULT NULL,
  `yorum` tinyint(1) DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_videoid` (`kategoriid`),
  FULLTEXT KEY `index_videotext` (`baslik`,`etiket`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `video`
--

/*!40000 ALTER TABLE `video` DISABLE KEYS */;
/*!40000 ALTER TABLE `video` ENABLE KEYS */;


--
-- Definition of table `yorum`
--

DROP TABLE IF EXISTS `yorum`;
CREATE TABLE `yorum` (
  `id` varchar(36) NOT NULL,
  `ip` varchar(15) DEFAULT NULL,
  `modulid` varchar(15) DEFAULT NULL,
  `icerikid` varchar(50) DEFAULT NULL,
  `adi` varchar(50) DEFAULT NULL,
  `mail` varchar(60) DEFAULT NULL,
  `icerik` varchar(500) DEFAULT NULL,
  `kayittarihi` datetime DEFAULT NULL,
  `yoneticionay` tinyint(1) DEFAULT NULL,
  `aktif` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `index_yorumid` (`modulid`,`icerikid`(40),`mail`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `yorum`
--

/*!40000 ALTER TABLE `yorum` DISABLE KEYS */;
INSERT INTO `yorum` (`id`,`ip`,`modulid`,`icerikid`,`adi`,`mail`,`icerik`,`kayittarihi`,`yoneticionay`,`aktif`) VALUES 
 ('3da30cbd-10c5-41bb-9d7e-6649a1de585c','::1','haber','4','Portal Yönetimi','admin@baymyo.com','Bayrağı buraya dikmeyi düşünen Muhtar Güneye ve destek veren tüm hemşehrilerine teşekkür eden Başkan Savaş, Şu gerçeği herkes bilmeli ki Hatay halkı bayrağına, namusuna, vatanına birlik ve beraberliğine de düşkün bir halktır.','2014-12-31 14:35:47',1,1);
/*!40000 ALTER TABLE `yorum` ENABLE KEYS */;

