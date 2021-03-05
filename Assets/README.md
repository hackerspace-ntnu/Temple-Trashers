## Rask guide til workflow
Utvikling gjøres i branches fra `develop`-branchen. Når en feature er ferdig merges branchen inn i `develop`.
Når flere sammengengende features er merget funksjonelt sammen i `develop`, pushes det til `main`-branchen. Dette gjøres
IKKE på egenhånd.


## Ting om Git og filtyper
Git er veldig bra på kode og tekstfiler. Dette inkluderer:
* Alt du manuelt koder (komponenter, scriptable objects, etc.)
* Alle tekstfiler

Disse filtypene kan merges uten problemer, selv om merging i seg selv ikke alltid er lett.

Git er ubrukelig på å merge filer som er laget av Unity eller annen programvare. Dette inkluderer:
* Scene-filer
* Prefab-filer
* Bildefiler
* Lydfiler
* 3D-modeller
* Materialer
* TOMMELFINGERREGEL: om du ikke skriver det manuelt inn, er Git sannsynligvis kranglete.

Når du skal merge disse filene, vil det å kombinere endringer fra flere branches ofte føre til krøll (merge conflicts).
For å skape minst mulig problemer gjøres dermed følgende:
* Ikke push endringer til scener med mindre featuren ER å utvikle scenen. Dvs. bare én gruppe gjør
  permanente endringer til scenen av gangen.
    - Om du er i tvil, lag en ny scene heller enn å endre på allerede eksisterende scener som andre bruker.
        - Unødvendige scener blir fjernet når de ikke lenger trengs.
    - Evt. kan du gjøre hva du vil med en scene så lenge du ikke pusher disse endringene (be careful!).
* Om du har endret på en scene/prefab og disse endringene skal med, sørg for at kun dine egne endringer blir pushet.


## Diverse
* Ikke skift tags på objekter andre har laget med mindre du VET at andre ikke har brukt taggen til noe. Tags blir
ofte brukt i kode for å vite objekttyper og liknende. Det å endre på dem kan føre til krøll.
* Ikke bruk navn til å sjekke objekttyper; dette er som regel det tags eller singeltons er for.
* Legg ting i riktig mappe under `Assets`-mappen; komponenter skal i `Scripts`, scener i `Scenes`, materialer i `Materials`, etc.
    - Lag gjerne undermapper for ting som omhandler samme funksjonalitet, f.eks. en `Player`-mappe
      for scripts som har med spilleren å gjøre.
    - Om det ikke er en mappe for typen, lag en.
