-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 14, 2024 at 12:47 AM
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
(1, '4Dm1n42', '1T1s4test'),
(9, 'test2', 'Testtest2\"'),
(103, 'test3', 'Testtest3£');

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
(9, 20),
(103, 21);

-- --------------------------------------------------------

--
-- Table structure for table `album`
--

CREATE TABLE `album` (
  `album_id` int(11) NOT NULL,
  `title` varchar(60) NOT NULL,
  `image_location` varchar(255) NOT NULL,
  `artist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `album`
--

INSERT INTO `album` (`album_id`, `title`, `image_location`, `artist_id`) VALUES
(5, 'Coral Fang', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\folder.jpg', 7);

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
(5, 1, 101),
(5, 2, 102),
(5, 3, 103),
(5, 4, 104),
(5, 5, 105),
(5, 6, 106),
(5, 7, 107),
(5, 8, 108),
(5, 9, 109),
(5, 10, 110),
(5, 11, 111);

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
(7, 'The Distillers');

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

-- --------------------------------------------------------

--
-- Table structure for table `playlist_track`
--

CREATE TABLE `playlist_track` (
  `playlist_id` int(11) NOT NULL,
  `track_number` int(11) NOT NULL,
  `song_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  `song_location` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `song`
--

INSERT INTO `song` (`song_id`, `name`, `song_location`) VALUES
(101, 'Drain The Blood', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\01DraintheBlood.m4a'),
(102, 'Dismantle Me', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\02DismantleMe.m4a'),
(103, 'Die on a Rope', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\03DieonaRope.m4a'),
(104, 'The Gallows is God', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\04TheGallowIsGod.m4a'),
(105, 'Coral Fang', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\05CoralFang.m4a'),
(106, 'The Hunger', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\06TheHunger.m4a'),
(107, 'Hall of Mirrors', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\07HallofMirrors.m4a'),
(108, 'Beat Your Heart Out', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\08BeatYourHeartOut.m4a'),
(109, 'Love is Paranoid', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\09LoveIsParanoid.m4a'),
(110, 'For Tonight Youre Only Here to Know', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\10ForTonightYoureOnlyHeretoKnow.m4a'),
(111, 'Deathsex', 'C:\\Users\\Craig\\Desktop\\Betawave8.0\\Betawave\\Resources\\Music\\CoralFang\\11Deathsex.m4a');

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
  ADD PRIMARY KEY (`album_id`),
  ADD KEY `fk_artist` (`artist_id`);

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
  MODIFY `account_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=120;

--
-- AUTO_INCREMENT for table `album`
--
ALTER TABLE `album`
  MODIFY `album_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `artist`
--
ALTER TABLE `artist`
  MODIFY `artist_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

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
  MODIFY `song_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=204;

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
-- Constraints for table `album`
--
ALTER TABLE `album`
  ADD CONSTRAINT `fk_artist` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`artist_id`);

--
-- Constraints for table `album_track`
--
ALTER TABLE `album_track`
  ADD CONSTRAINT `album_track_to_album` FOREIGN KEY (`album_id`) REFERENCES `album` (`album_id`),
  ADD CONSTRAINT `album_track_to_song` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`);

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
