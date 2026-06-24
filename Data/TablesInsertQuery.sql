USE rehearsaldb;

-- Users

INSERT INTO Roles (Name) VALUES
('Admin'),
('User');

INSERT INTO Users (FirstName, LastName, Phone, Email, PasswordHash, RoleId) VALUES
(N'Вячеслав', N'Завадский', '8 975 135 92 01', 'vaclavkey@gmail.com', 'VACHASH', 1),
(N'Сергей',   N'Бусыгин',   '8 495 482 12 53', 'seriybus@gmail.com', 'BUSCASH', 1),
(N'Даниил',   N'Дурнев',    '8 942 552 99 13', 'doradura@gmail.com', 'DURACASH', 2),
(N'Пабло',    N'Быжбыж',   '8 932 726 13 63', 'bzhbzh@gmail.com', 'BZHCASH', 2),
(N'Гео', N'Надзиратель', '8 777 228 13 37', 'ricarsmerty@gmail.com', 'DEDCASH', 1);

-- Instruments --

INSERT INTO InstrumentTypes (Name) VALUES
('Guitar'),
('Percussion'),
('Keyboard');

INSERT INTO Instruments (Name, InstrumentTypeId) VALUES
('6-String Electric Guitar', 1),
('7-String Electric Guitar', 1),
('4-String Bass Guitar', 1),
('5-String Bass Guitar', 1),
('Drum Kit', 2);

INSERT INTO Tunings (Name, Notes) VALUES
('Drop C', 'C G C F A D'),
('Drop A', 'A E A D G B E'),
('Drop B', 'B F# B E G# C#');

INSERT INTO InstrumentTunings (InstrumentId, TuningId) VALUES
(1, 1),
(1, 3),
(2, 2);


-- Bands

INSERT INTO Bands (Name) VALUES
('Destination Point'),
('Snow Absorbs Blood'),
('Maudlin Rust'),
(N'Аферист'),
('Fast Food'),
('Durnev');

INSERT INTO BandRoles (Name) VALUES
('Lead Guitarist'),
('Rhytm Guitarist'),
('Drummer'),
('Bassist'),
('Singer');

INSERT INTO Genres (Name) VALUES
('Metal'),
('Heavy Metal'),
('Metalcore'),
('Deathcore'),
('Beatdown'),
('Nu-Metal'),
('Melodic Death Metal'),
('Groove Metal'),
('Alternative Metal'),
('Rapcore'),
('Rock'),
('Hard Rock'),
('Punk'),
('Jazz'),
('Blues'),
('Indie'),
('Alternative'),
('Pop');

INSERT INTO BandMembers (BandId, UserId, BandRoleId) VALUES
(1, 1, 3),
(1, 2, 5),
(3, 1, 3),
(3, 3, 2);

INSERT INTO BandGenres (BandId, GenreId) VALUES
(1, 1),
(1, 3),
(1, 9),
(3, 1),
(3, 3),
(3, 4),
(3, 9);

-- Songs

INSERT INTO SongStatuses (Name) VALUES
('Draft'),
('In Progress'),
('Ready For Live'),
('Release');

INSERT INTO Songs (UserId, BandId, TuningId, SongStatusId, Name, TimeSeconds, CoverImage) VALUES
(1, 1, 1, 1, 'Werewolf: Chapter II', 230, 'werewolf_2_cover.png'),
(2, 1, 1, 1, 'His Day Will Come', 250, 'he_is_devils_cum_cover.png'),
(1, 3, 1, 1, 'The Armor Weeps', 310, 'the_armor_weeps_cover.png'),
(1, 3, 1, 1, 'White Noize', 269, 'white_noize_cover.png');

INSERT INTO Setlists (Name, BandId) VALUES
('Metal Mystery 5', 1),
('Peak Sound Gig', 3);

INSERT INTO SetlistSongs (SetlistId, SongId) VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4);

INSERT INTO Tabs (Name, Url, SongId, InstrumentId, TuningId) VALUES
('The Armor Weeps Rhytm Guitar Tab', 'https://songsterr/maudlin_rust/The_Armor_Weeps_Tabs/Rhytm_Guitar', 3, 1, 3),
('Werewolf: Chapter II Lead Guitar Tab', 'https://songsterr/destination_point/Werewolf_Chapter_II_Tabs/Lead_Guitar', 1, 2, 2);

-- Rehearsals

INSERT INTO Places (Name, Address) VALUES
(N'Red Gates', N'Улица Правды 24с3'),
(N'Under The Ground', N'Улица Правды 24с3'),
(N'High Gain', N'Улица Правды 24с3'),
(N'Nazarov Studio', N'Улица Правды 24с3'),
(N'MuzProf', N'Улица Правды 24с3'),
(N'Hendrix Studio', N'Улица Правды 24с3');

INSERT INTO Ratings (Name) VALUES
('Needs Work'),
('Acceptable'),
('Good'),
('Very Good'),
('Perfomance Ready');

INSERT INTO RehearsalStatuses (Name) VALUES 
('Planned'),
('In Progress'),
('Completed'),
('Cancelled');

INSERT INTO Rehearsals (Name, BandId, SetlistId, PlaceId, RehearsalStatusId, Date, TimeSeconds, Note) VALUES
('Генеральный прогон перед Metal Mystery 5', 1, 1, 1, 1, '2026-07-01', 21600, 'Жесточайше ебашим'),
('Прогон перед гигом', 3, 2, 2, 1, '2026-07-12', 10800, 'Нежнейший шоколадный маффин с черничной начинкой и шоколадной крошкой');

INSERT INTO RehearsalSongs (RehearsalId, SongId, RatingId) VALUES
(1, 1, 4),
(1, 2, 3),
(2, 3, 5),
(2, 4, 4);

INSERT INTO RehearsalMemberStatuses (Name) VALUES
('Present'),
('Late'),
('Absent');

INSERT INTO RehearsalMembers (RehearsalId, UserId, RehearsalMemberStatusId, BandMemberId, BandId) VALUES
(1, 1, 1, 1, 1),
(1, 2, 2, 2, 1);

