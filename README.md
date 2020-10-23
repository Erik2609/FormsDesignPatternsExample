# FormsDesignPatternsExample

## Duplicate logica weghalen
Beide strategieen hebben nog logica om de user context op te slaan. Ik zou dit laatste oplossen met een decorator pattern (https://en.wikipedia.org/wiki/Decorator_pattern).
De decorator is een manier om genest logica op te bouwen. In dit geval hebben we bijvoorbeeld een adres strategy, maar daarbovenop zou nog een audit strategy kunnen komen (en in de toekomst wellicht meer).

Omdat de factory in eerste instatie alleen informatie heeft over de formulier naam, breiden we deze uit met de instantie van het formulier, hierdoor kunnen ook de velden op het formulier bekeken worden. Vervolgens voegen we aan de factory toe dat als het formulier de audit velden bevat, dat deze ook de audit logica moet toepassen bij het opslaan.
