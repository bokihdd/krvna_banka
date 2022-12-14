-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 23, 2022 at 10:57 AM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bankakrvi2`
--

-- --------------------------------------------------------

--
-- Table structure for table `donacije`
--

CREATE TABLE `donacije` (
  `idDonacije` int(11) NOT NULL,
  `idPacijenta` int(11) NOT NULL,
  `idKrvneGrupe` int(11) NOT NULL,
  `KolicinaKrviDonirano` int(11) NOT NULL,
  `DatumDonacije` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `donor`
--

CREATE TABLE `donor` (
  `IdDonora` int(11) NOT NULL,
  `Ime` varchar(50) NOT NULL,
  `Prezime` varchar(50) NOT NULL,
  `idKrvneGrupe` int(11) NOT NULL,
  `KolicinaKrvi` int(11) NOT NULL,
  `DatumDoniranja` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `inventar`
--

CREATE TABLE `inventar` (
  `idInventara` int(11) NOT NULL,
  `idKrvneGrupe` int(11) NOT NULL,
  `UkupnaKolicinaKrvi` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `inventar`
--

INSERT INTO `inventar` (`idInventara`, `idKrvneGrupe`, `UkupnaKolicinaKrvi`) VALUES
(1, 1, 0),
(2, 2, 0),
(3, 3, 0),
(4, 4, 0),
(5, 5, 0),
(6, 6, 0),
(7, 7, 0),
(8, 8, 0);

-- --------------------------------------------------------

--
-- Table structure for table `korisnik`
--

CREATE TABLE `korisnik` (
  `idKorisnika` int(11) NOT NULL,
  `Ime` varchar(50) NOT NULL,
  `Prezime` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `KorisnickoIme` varchar(50) NOT NULL,
  `Sifra` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `krvne_grupe`
--

CREATE TABLE `krvne_grupe` (
  `idKrvneGrupe` int(11) NOT NULL,
  `Naziv` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `krvne_grupe`
--

INSERT INTO `krvne_grupe` (`idKrvneGrupe`, `Naziv`) VALUES
(1, 'A+'),
(2, 'A-'),
(3, 'B+'),
(4, 'B-'),
(5, 'AB+'),
(6, 'AB-'),
(7, 'O+'),
(8, 'O-');

-- --------------------------------------------------------

--
-- Table structure for table `pacijent`
--

CREATE TABLE `pacijent` (
  `idPacijenta` int(11) NOT NULL,
  `Ime` varchar(50) NOT NULL,
  `Prezime` varchar(50) NOT NULL,
  `idKrvneGrupe` int(11) NOT NULL,
  `PotrebnaKolicinaKrvi` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `donacije`
--
ALTER TABLE `donacije`
  ADD PRIMARY KEY (`idDonacije`),
  ADD UNIQUE KEY `idPacijenta` (`idPacijenta`),
  ADD KEY `idKrvneGrupe` (`idKrvneGrupe`);

--
-- Indexes for table `donor`
--
ALTER TABLE `donor`
  ADD PRIMARY KEY (`IdDonora`),
  ADD KEY `idKrvneGrupe` (`idKrvneGrupe`);

--
-- Indexes for table `inventar`
--
ALTER TABLE `inventar`
  ADD PRIMARY KEY (`idInventara`),
  ADD UNIQUE KEY `idKrvneGrupe` (`idKrvneGrupe`);

--
-- Indexes for table `korisnik`
--
ALTER TABLE `korisnik`
  ADD PRIMARY KEY (`idKorisnika`);

--
-- Indexes for table `krvne_grupe`
--
ALTER TABLE `krvne_grupe`
  ADD PRIMARY KEY (`idKrvneGrupe`);

--
-- Indexes for table `pacijent`
--
ALTER TABLE `pacijent`
  ADD PRIMARY KEY (`idPacijenta`),
  ADD KEY `idKrvneGrupe` (`idKrvneGrupe`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `donacije`
--
ALTER TABLE `donacije`
  MODIFY `idDonacije` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `donor`
--
ALTER TABLE `donor`
  MODIFY `IdDonora` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `inventar`
--
ALTER TABLE `inventar`
  MODIFY `idInventara` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `korisnik`
--
ALTER TABLE `korisnik`
  MODIFY `idKorisnika` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `krvne_grupe`
--
ALTER TABLE `krvne_grupe`
  MODIFY `idKrvneGrupe` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `pacijent`
--
ALTER TABLE `pacijent`
  MODIFY `idPacijenta` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `donacije`
--
ALTER TABLE `donacije`
  ADD CONSTRAINT `donacije_ibfk_1` FOREIGN KEY (`idPacijenta`) REFERENCES `pacijent` (`idPacijenta`),
  ADD CONSTRAINT `donacije_ibfk_2` FOREIGN KEY (`idKrvneGrupe`) REFERENCES `krvne_grupe` (`idKrvneGrupe`);

--
-- Constraints for table `donor`
--
ALTER TABLE `donor`
  ADD CONSTRAINT `donor_ibfk_1` FOREIGN KEY (`idKrvneGrupe`) REFERENCES `krvne_grupe` (`idKrvneGrupe`);

--
-- Constraints for table `inventar`
--
ALTER TABLE `inventar`
  ADD CONSTRAINT `inventar_ibfk_1` FOREIGN KEY (`idKrvneGrupe`) REFERENCES `krvne_grupe` (`idKrvneGrupe`);

--
-- Constraints for table `pacijent`
--
ALTER TABLE `pacijent`
  ADD CONSTRAINT `pacijent_ibfk_1` FOREIGN KEY (`idKrvneGrupe`) REFERENCES `krvne_grupe` (`idKrvneGrupe`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
