# FormsDesignPatternsExample

## Eerste gedachte
Wat mij opvalt is dat de methodes beide steeds groter zouden worden als er meer formulieren worden toegevoegd. Voor extra formulieren moeten bij beide formulieren extra if statements gemaakt worden, wat uiteindelijk ervoor zou zorgen dat de controller onoverzichtelijk wordt, zelfs als we de duplicate code naar methodes zouden verplaatsen. Ik zou hier een strategy pattern voor inzetten (https://en.wikipedia.org/wiki/Strategy_pattern)

## Voordelen strategy pattern
De logica voor de formulieren kan in een eigen class geplaatst worden, als je het adres formulier zou moeten debuggen, hoeft je niet meer te kijken naar de logica van het persoonlijke gegevens formulier.

Ook biedt het strategy pattern de mogelijkheid om logica te bouwen voor beide methodes in 1 class, dit biedt overzicht over wat er specifiek aan dit formulier is.


## Factory pattern
Vaak als in een strategy pattern inzet, combineer ik deze met een factory pattern (https://en.wikipedia.org/wiki/Factory_method_pattern). De factory kan op basis van het formulier bepalen welke strategy nodig is en geeft de concrete implementatie terug aan de controller. Het voordeel is, de controller blijft simpel en de logica is makkelijk herbruikbaar door een nieuwe controller die bijvoorbeeld ook formulieren moet kunnen opslaan.

### Vervolg
We houden wel nog wat duplicate code over, beide strategies hebben nog steeds logica om de gebruiker context op te slaan bij het formulier.
De laaste stap is terug te zien op de branch add-decorator-pattern
