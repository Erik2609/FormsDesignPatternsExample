# FormsDesignPatternsExample

Dit is een voorbeeld project van een use-case die ik een aantal keer ben tegengekomen. Het project zelf is wat eenvoudig gehouden en de focus ligt het meest op hoe de FormsController wordt gebruikt.

in de master branch is de begin situatie geschetst.
Er is een generieke API om formulieren in te schieten en om de formulieren van pre-fill gegevens te voorzien.
GetPrefillData en SubmitForm.

Wat opvalt is dat er wat uitzondering formulieren bestaan, namelijk het adres formulier en het persoonlijke gegevens formulier.
Deze bevatten beide extra logica bij het ophalen van de pre-fill data en bij het opslaan.

## Nieuwe wens

Je krijgt de opdracht om nog een formulier toe te voegen aan deze generieke API, met weer custom logica bij het pre-fillen en bij het inschieten. Het formulier dat gemaakt moet worden moet ook gecontroleerd kunnen worden met de actieve gebruiker gegevens conform de adres- en persoonlijke gegevens formulieren.

## Refactoren

Ga na wat je zou refactoren voordat je aan de nieuwe wens zou beginnen.


### Benieuwd naar mijn oplossing?
Bekijk de branch add-strategy-pattern
