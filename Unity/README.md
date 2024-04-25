# EiT Prosjekt | Virtuell Teoriprøve for Statens Vegvesen

Laget og utviklet av:

* Anna Holden Jacobsen
* Caroline Grimsrud
* Christian Le
* Harald Bjerkeli
* Olav Seim

## EiT Emneinfo

* Navn: VR/AR og AI for læring
* Emnekode: PED3801
* Gruppe: 2

## Teknisk info

Den virtuelle teoriprøven er utviklet i Unity og er en MVP/Minimal Viable Product/Proof of Concept på virtuell teoriprøve.

Mesteparten av prosjektet er bygget opp fra bunnen av bortsett fra input control manageren som kommer fra Unity sine standard assets: XR Interaction Toolkit.
Mer info om denne kan sjekkes ut [her](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html).

### C# Kode

Koden er for øyeblikket skrevet med intensjon om at generering av nye scener, bevegelser og spørsmål kan alt gjøres i editoren. Modifikasjoner til disse kan gjøres om det er ønsker om endring til bevegelser og spørsmål.

Fire ulike scripts styrer for øyeblikket spillet:

1. [CarControl.cs](Assets/Scripts/CarControl.cs)
2. [PlayerScript.cs](Assets/Scripts/PlayerScript.cs)
3. [XRCarClick.cs](Assets/Scripts/XRCarClick.cs)
4. [SceneChanger.cs](Assets/Scripts/SceneChanger.cs)

#### CarControl.cs

Denne koden håndterer handlingene til bilen basert på hvilken tilstand den er i. Et eksempel av struktur av handlinger kan sees i "MainCar/CarControl" komponenten som er en sekvens av handlinger "List<Action> action_sequences", en for hvert spørsmål. En handling kan bestå av en sekvens av bevegelser "List<MovementProperties> movementProperties".

Movement properties beskriver hva beveglsen skal gjøre:

1. **distance**: Lengden bilen skal bevege seg rett fram
2. **smoothIn**: Om hastigheten skal gå fra 0 -> "velocity" i løpet av bevegelsen
3. **smoothOut**: Om hastigheten skal gå fra "velocity" -> 0 i løpet av bevegelsen
    * Hvis begge to hukes av går den fra 0 -> "velocity" -> 0, i løpet av bevegelsen
4. **isPivot**: Huk av om bevegelsen skal være en sving/sirkelbevegelse. 
5. **isLeft**: Om isPivot er huket av, så bestemmer denne om du skal svinge mot venstre. Om avhuket, svinger bilen mot høyre
6. **degrees**: Antall grader rotasjonen skal gå
7. **standby**: Hvor mange sekunder bilen bare skal stå stille

Prioriteten i type bevegelse er: "standby" > "isPivot" > "distance".

Som betyr at definert "standby" vil 'override' spesifisert "isPivot" og "distance", osv...

Strukturen på en hel bevegelse er definert slik ettersom alle type baner kan i teorien bli laget av kun rette linjer og sirkler av ulik radius.

Formålet med "state" mekanismen er at for hver gang man kommer videre fra et spørsmål (og startknappen i begynnelsen) kommer bilen i en ny tilstand. Tenk det som at på starten er bilen i en "start av spill" tilstand, der når spilleren trykker start kommer den i en "spørsmål 1" tilstand, osv...

#### PlayerScript.cs

Har ansvaret for formattering av spørsmål og feedback. Lignende som "CarControl.cs", struktureres spørsmålene i lister etter antall oppgaver.

Kontakt Harald for mer detaljert informasjon på denne.

#### XRCarClick.cs

Denne koden håndterer det som skjer når spilleren interagerer med bilen. For øyeblikket får den sentrum av spilleren (kroppen) til å bli festet til bilsetet og fjerner muligheter for å teleportere ettersom det nå blir fokus på å svare på spørsmål.

#### SceneChanger.cs

Denne lille koden sørger for å laste inn ulike scener. F.eks. når vi starter Scenario 1, så laster den inn denne.

### Unity Editor

Hovedkomponentene til [Scenario 1](Assets/Scenes/Scenario1.unity) og [Scenario 2](Assets/Scenes/Scenario2.unity) er:

* **Objects**: Inneholder veier, skilt og bygninger.
* **TriggerPlanes**: Inneholder "grenser" som aktiverer spørsmål og "exit" til hovedmeny når bilen går gjennom disse.
* **Complete XR Origin Set up Variant**: Inneholder alle komponenter relatert til VR kontrollerne, meste er "blackbox" som er implementert av Unity med småmodifikasjoner av oss.
* **PlayerProps**: Er inne i "Complete XR Origin Set up Variant/XR Origin(XR Rig)/Camera Offset/Main Camera/PlayerProps" som inneholder spørsmål og feedback.
* **MainCar**: Inneholder bilen og dens bevegelser. Styres av "CarControl.cs".
* **OtherCars**: Inneholder andre biler som kjører rundt i scenen. Styres av "CarControl.cs".
* **Terrain**: Inneholder terrenget til verden.
* **BackgroundMusic**: Inneholder bakgrunnsmusikken til spillet.
* **SceneChanger**: Inneholder "SceneChanger.cs" scriptet som laster inn ulike scener.
* **XR Device Simulator**: Inneholder "XR Device Simulator" som er en simulator for VR kontrollerne. SKRU DENNE AV FØR DU BYGGER SPILLET, BRUKES KUN FOR TESTING I EDITOR.
* **Player HUD UI**: Befinner seg i "MainCar" og er UI elementene som viser spørsmål og feedback.
