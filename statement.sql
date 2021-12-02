use [master];
GO

CREATE DATABASE CardApp;
GO

USE CardApp;
GO 

CREATE TABLE Cards (
	ID int IDENTITY(1,1) PRIMARY KEY,
	CardName varchar(200),
	SetName varchar(3),
	NumberOwned int,
	IsFoil BIT,
	SetNumber int,
	ImageUrl varchar(255),
	NormPrice decimal(10,2),
	FoilPrice decimal(10,2),
 );

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Cabal Coffers', 'TOR', 1, 0, 139, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/2/b/2b934c78-258e-4b1e-9783-ec9f734e8776.jpg?1562629046', 
123.53, 193.82);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Metalworker', 'UDS', 1, 0, 135, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/2/0/2050d414-71c7-4c42-a1ff-4c04068ba7f2.jpg?1562443733', 
187.14, 699.99);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Mana Drain', 'CMR', 1, 0, 80, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/b/a/ba874c0c-f66f-4edc-9859-40273487aef0.jpg?1608909310', 
47.38, 57.99);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Lord Windgrace', 'C18', 1, 1, 43, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/2/1/213d6fb8-5624-4804-b263-51f339482754.jpg?1592710275', 
0, 6.93);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Nurturing Peatland', 'MH1', 1, 0, 243, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/2/7/2744ac83-a79f-4042-8720-688b5adda382.jpg?1562202580', 
9.90, 40.35);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Demonic Tutor', 'STA', 1, 0, 27, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/3/0/3009ba46-c9f8-46dc-8ffc-2aa4cef7b17c.jpg?1619158025', 
40.17, 82.91);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Vampiric Tutor', 'VIS', 1, 0, 72, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/0/a/0a07cba3-2e8d-48ec-a6f8-4d2edfcd833d.jpg?1562276960', 
76.35, 0);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Lion''s Eye Diamond', 'MIR', 1, 0, 307, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/6/3/63bacc32-d6ba-420c-9b49-299c08e5fb39.jpg?1562719750', 
556.34, 0);

INSERT INTO Cards(CardName, SetName, NumberOwned, IsFoil, SetNumber, ImageUrl, NormPrice, FoilPrice) 
VALUES('Kaya the Inexorable', 'KHM', 1, 1, 288, 
'https://c1.scryfall.com/file/scryfall-cards/border_crop/front/3/5/35e3a516-2989-4cac-892f-deb3d754ba7d.jpg?1616295947', 
5.41, 9.40);
