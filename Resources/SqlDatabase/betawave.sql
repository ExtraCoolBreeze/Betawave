-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 18, 2024 at 03:47 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `betawave`
--

-- --------------------------------------------------------

--
-- Table structure for table `account`
--

CREATE TABLE `account` (
  `account_id` int(11) NOT NULL,
  `username` varchar(60) NOT NULL,
  `userpassword` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account`
--

INSERT INTO `account` (`account_id`, `username`, `userpassword`) VALUES
(1, '4Dm1n42', '1t1s4test'),
(9, 'test2', '0aa84d5589f317f4e7cf7695da9c340d351fa678e77a075e8d747defe737d68f'),
(10, 'testing', '7ef030b1f272d461f1d25e647add9443c24d6660bc91477607cdf75b216289df'),
(11, 'testing2', '7ef030b1f272d461f1d25e647add9443c24d6660bc91477607cdf75b216289df'),
(12, 'Admin', '1bb9567ce8d4ba06e7fb6e9121a466c43797d4a1cf2dbeda931747c981110b1a'),
(13, 'Source road form huge garden. Mr bank state write power.', 'Cost treat agree stage but.'),
(20, 'user1', 'pass1'),
(21, 'user2', 'pass2'),
(23, 'Place forget land.', 'Policy red ability physical discover cell.'),
(28, 'Man method until order walk decide.', 'Account eight push. Capital trip mind energy with four.'),
(36, 'Top travel serious. Represent grow PM get ground.', 'Measure specific president another list mind.'),
(48, 'Direction lot large try edge. Pretty bar next car.', 'Almost statement type Democrat season.'),
(53, 'Increase south city. Meet chair food.', 'Speak measure live huge attack.'),
(55, 'Could practice usually trouble. Above actually spend.', 'Position defense bad cut.'),
(61, 'Administration everything animal Mr turn quickly part.', 'Outside seat series step collection center central art.'),
(65, 'Inside dark market. West quickly they.', 'Down finish ok check.'),
(73, 'Speak realize suffer society Democrat.', 'Strategy particularly others where better hundred.'),
(75, 'Whom season none catch. Indeed my probably second.', 'Read that sound market care manager raise.'),
(84, 'Fly federal thought represent them while include.', 'Court feeling father. Dream key century adult.'),
(90, 'Difficult suggest analysis however ready from.', 'Far discussion class ago. Safe positive record.'),
(94, 'Security officer for product after discussion town.', 'I thus whom say someone deep. Food impact concern writer.'),
(97, 'Drop identify arm those agree my security several.', 'Size no gun trip. Can up like culture.'),
(98, 'Paul1', '1e053fa627b56ca829d34d13229768f7f5b2c26960d6ccab84ddf0f620b737c0');

-- --------------------------------------------------------

--
-- Table structure for table `account_role`
--

CREATE TABLE `account_role` (
  `account_id` int(11) NOT NULL,
  `role_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `account_role`
--

INSERT INTO `account_role` (`account_id`, `role_id`) VALUES
(1, 1),
(9, 20);

-- --------------------------------------------------------

--
-- Table structure for table `album`
--

CREATE TABLE `album` (
  `album_id` int(11) NOT NULL,
  `title` varchar(60) NOT NULL,
  `image_location` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `album`
--

INSERT INTO `album` (`album_id`, `title`, `image_location`) VALUES
(1, 'Album One', 'play.png'),
(2, 'Album Two', 'list.png'),
(3, 'Album One', 'play.png'),
(4, 'Album Two', 'list.png');

-- --------------------------------------------------------

--
-- Table structure for table `album_track`
--

CREATE TABLE `album_track` (
  `album_id` int(11) NOT NULL,
  `track_number` int(11) NOT NULL,
  `song_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `album_track`
--

INSERT INTO `album_track` (`album_id`, `track_number`, `song_id`) VALUES
(1, 7, 1),
(2, 4, 2);

-- --------------------------------------------------------

--
-- Table structure for table `artist`
--

CREATE TABLE `artist` (
  `artist_id` int(11) NOT NULL,
  `name` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `artist`
--

INSERT INTO `artist` (`artist_id`, `name`) VALUES
(1, 'John Doe'),
(2, 'Jane Smith'),
(3, 'Artist One'),
(4, 'Artist Two');

-- --------------------------------------------------------

--
-- Table structure for table `featured_artists`
--

CREATE TABLE `featured_artists` (
  `artist_id` int(11) NOT NULL,
  `song_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `featured_artists`
--

INSERT INTO `featured_artists` (`artist_id`, `song_id`) VALUES
(1, 2),
(2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `playlist`
--

CREATE TABLE `playlist` (
  `playlist_id` int(11) NOT NULL,
  `title` varchar(60) NOT NULL,
  `queue` varchar(6) NOT NULL,
  `favourite` varchar(6) NOT NULL,
  `account_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `playlist`
--

INSERT INTO `playlist` (`playlist_id`, `title`, `queue`, `favourite`, `account_id`) VALUES
(1, 'Queue', 'yes', 'no', 9),
(2, 'Favourite', 'no', 'yes', 9);

-- --------------------------------------------------------

--
-- Table structure for table `playlist_track`
--

CREATE TABLE `playlist_track` (
  `playlist_id` int(11) NOT NULL,
  `track_number` int(11) NOT NULL,
  `song_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `playlist_track`
--

INSERT INTO `playlist_track` (`playlist_id`, `track_number`, `song_id`) VALUES
(1, 7, 1),
(2, 4, 2);

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `role_id` int(11) NOT NULL,
  `admin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`role_id`, `admin`) VALUES
(1, 1),
(20, 1),
(21, 0);

-- --------------------------------------------------------

--
-- Table structure for table `song`
--

CREATE TABLE `song` (
  `song_id` int(11) NOT NULL,
  `name` varchar(60) NOT NULL,
  `duration` varchar(60) NOT NULL,
  `song_location` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `song`
--

INSERT INTO `song` (`song_id`, `name`, `duration`, `song_location`) VALUES
(1, 'Song One', '4:20', 'list.png'),
(2, 'Song Two', '7:40', 'play.png'),
(8, 'Similar finally property high TV. Not Congress body.', 'Despite manage room represent.', 'Outside play section free road threat especially.'),
(14, 'Way system pick current black example own.', 'Yard dinner take least world. Score mind pay difference.', 'forward.jpg'),
(31, 'Account simple Mrs central on middle. Before prove TV.', 'Throughout smile discuss minute understand.', 'Suddenly pressure star drop sit structure.'),
(34, 'Tell board them laugh phone.', 'End practice describe worry find evidence require.', 'Image reach play start.'),
(37, 'President outside opportunity certainly number.', 'Carry Mr investment. Wind can more would.', 'Attorney on road prove forward.'),
(40, 'Beat discuss but service national character.', 'Street short performance site city free bad age.', 'list.jpg'),
(48, 'Drop modern arrive list official process.', 'Set these hot of white. Summer seat well someone make.', 'Memory happy game century.'),
(64, 'Piece individual heavy economy.', 'Should toward bit audience reduce every.', 'Prepare population modern value explain religious attack.'),
(70, 'Capital purpose administration drop beyond.', 'Consumer team nation front have such.', 'Throughout worry tell southern because local.'),
(71, 'Ever available least before wait. Total save yard she.', 'Stop hit thus safe reality give.', 'Not play after a professional.'),
(75, 'Least yourself including technology challenge.', 'Center light model voice to decade.', 'Morning record boy enough last view difference include.'),
(83, 'Blue by yeah cover base.', 'Political time skin you wall.', 'Hope friend after show whom most respond.'),
(89, 'Star management hospital perform product social.', 'Sometimes wide PM seat development themselves back.', 'Miss professor occur short design.');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `account`
--
ALTER TABLE `account`
  ADD PRIMARY KEY (`account_id`);

--
-- Indexes for table `account_role`
--
ALTER TABLE `account_role`
  ADD PRIMARY KEY (`account_id`,`role_id`),
  ADD KEY `account_role_to_role` (`role_id`);

--
-- Indexes for table `album`
--
ALTER TABLE `album`
  ADD PRIMARY KEY (`album_id`);

--
-- Indexes for table `album_track`
--
ALTER TABLE `album_track`
  ADD PRIMARY KEY (`album_id`,`track_number`),
  ADD KEY `album_track_to_song` (`song_id`);

--
-- Indexes for table `artist`
--
ALTER TABLE `artist`
  ADD PRIMARY KEY (`artist_id`);

--
-- Indexes for table `featured_artists`
--
ALTER TABLE `featured_artists`
  ADD PRIMARY KEY (`artist_id`,`song_id`),
  ADD KEY `featured_artists_to_song` (`song_id`);

--
-- Indexes for table `playlist`
--
ALTER TABLE `playlist`
  ADD PRIMARY KEY (`playlist_id`),
  ADD KEY `playlist_to_account` (`account_id`);

--
-- Indexes for table `playlist_track`
--
ALTER TABLE `playlist_track`
  ADD PRIMARY KEY (`playlist_id`,`track_number`),
  ADD KEY `playlist_track_to_song` (`song_id`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`role_id`);

--
-- Indexes for table `song`
--
ALTER TABLE `song`
  ADD PRIMARY KEY (`song_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `account`
--
ALTER TABLE `account`
  MODIFY `account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=99;

--
-- AUTO_INCREMENT for table `album`
--
ALTER TABLE `album`
  MODIFY `album_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `artist`
--
ALTER TABLE `artist`
  MODIFY `artist_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `playlist`
--
ALTER TABLE `playlist`
  MODIFY `playlist_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `song`
--
ALTER TABLE `song`
  MODIFY `song_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=90;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `account_role`
--
ALTER TABLE `account_role`
  ADD CONSTRAINT `account_role_to_account` FOREIGN KEY (`account_id`) REFERENCES `account` (`account_id`),
  ADD CONSTRAINT `account_role_to_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`);

--
-- Constraints for table `album_track`
--
ALTER TABLE `album_track`
  ADD CONSTRAINT `album_track_to_album` FOREIGN KEY (`album_id`) REFERENCES `album` (`album_id`),
  ADD CONSTRAINT `album_track_to_song` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`);

--
-- Constraints for table `featured_artists`
--
ALTER TABLE `featured_artists`
  ADD CONSTRAINT `featured_artists_to_artist` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`artist_id`),
  ADD CONSTRAINT `featured_artists_to_song` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`);

--
-- Constraints for table `playlist`
--
ALTER TABLE `playlist`
  ADD CONSTRAINT `playlist_to_account` FOREIGN KEY (`account_id`) REFERENCES `account` (`account_id`);

--
-- Constraints for table `playlist_track`
--
ALTER TABLE `playlist_track`
  ADD CONSTRAINT `playlist_track_to_playlist` FOREIGN KEY (`playlist_id`) REFERENCES `playlist` (`playlist_id`),
  ADD CONSTRAINT `playlist_track_to_song` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
