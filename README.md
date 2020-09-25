# Tesztvezérelt programozás oktatás

*BSc szakdolgozat*

A tesztvezérelt fejlesztés (Test Driven Development) egy előnyös módszernek bizonyult a szoftverfejlesztés területén. A dolgozat a programozás oktatása esetében vizsgálja azt, hogy a tesztek készítésének, az azokkal történő számonkérésnek milyen előnyei, esetlegesen hátrányai lehetnek.

A dolgozat bemutatja egy webalkalmazás elkészítését, annak funkcióit, amelyben az elkészített tananyagokat el lehet helyezni, a diákok/hallgatók szerkeszthetik és ellenőrízhetik a feladatokhoz tartozó programkódokat. A program figyelmezteti a felhasználót a gyakran előforduló hibákkal kapcsolatban. A program a feladat elkészítését nyomonköveti, segítve ezzel az értékelést és a feladatok megoldási módjának vizsgálatát.

Az alkalmazás szerver oldali része ASP.NET keretrendszerben készül. A böngészőben futó kliens alkalmazás elkészítéséhez Angular JavaScript keretrendszer kerül felhasználásra. Ennek megfelelően a dolgozatban szereplő oktatási anyagok, kódpéldák szintén C# és JavaScript nyelven készülnek.

## Ötletek, tervezett lépések

Az alkalmazás Angular 10 és ASP.NET Core (.NET Core 3.1) technológiákkal készülne. A szakdolgozat fejlesztési fázisának első lépése az lenne, hogy kitaláljuk majd lefejlesztjük milyen módon lehet a C# környezetben programmatikus módon előre meghatározott egységteszteket futtatni a felhasználótól megkapott forráskódra. A megkapott forráskód programozási nyelve akár lehet C#, vagy más is, ha van idő rá és ha a kódot sikerül megfelelően absztraktálni akkor lehetne implementálni egy második nyelvet is, akár JavaScript/TypeScript vagy C/C++.

A egységteszt alapú ellenőrzéshez az egységteszteket és a hozzátartozó adatokat kellene tárolni.
A hozzátartozó adatok alatt értem:
* egy optimális megoldást (ami a kitöltő számára nem látszik),
* egy programvázat, ami a metódusok prototípusát (név, argumentumok, visszatérési érték) és esetlegesen a használt függvénykönyvtárakat tartalmazná és,
* egy feladatleírást.

A fejlesztési fázis második lépése az lenne, hogy készítünk hozzá egy ASP.NET backend-et és egy Angular frontend-et. Arra gondoltam, hogy lehetne tanárként és diákként is belépni a rendszerbe. Tanár jogosultsággal új feladatokat tölthetünk fel és oszthatunk ki csoportok/tankörök számára. Diákként pedig a hozzánk rendelt feladatokat látnánk és a korábbi feladatainkat.

A forráskód esetén lefutnak az egységtesztek és különböző statisztikák kiértékelése is megtörténik. A kitöltő megkapja az egységteszt lefutása után a naplófájlt szépen formázva, csak a számára fontos adatokkal. Például hány teszt volt sikeres, milyen kivételt dobott a kódja, stb. Lehetne korlátozni a futási időt, és használható memóriát is. A program tudna neki segíteni hogy ha valamilyen nem szükséges függvénykönyvtárt használ, vagy túl hosszú egy metódusa. Valamilyen algoritmussal lehetne pontozni és százalékolni a beküldött feladatot. Többszöri feltöltés esetén látná hogy az előzőekhez képest mennyit tudott javítani.

Ezeket az adatokat tanári oldalon is meg lehet jeleníteni, de szerintem ez nem szükséges.

A CodeWars ehhez hasonló alkalmazás, azt és az ELTE rendszerét össze lehetne hasonlítani a szakdolgozatban megírt alkalmazással.
* https://www.codewars.com
* https://github.com/codewars/codewars-runner-cli

