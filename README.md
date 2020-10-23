# FormsDesignPatternsExample

## Duplicate logica weghalen
Beide strategieen hebben nog logica om de user context op te slaan. Ik zou dit laatste oplossen met een decorator pattern (https://en.wikipedia.org/wiki/Decorator_pattern).
De decorator is een manier om genest logica op te bouwen. In dit geval hebben we bijvoorbeeld een adres strategy, maar daarbovenop zou nog een audit strategy kunnen komen (en in de toekomst wellicht meer).

Omdat de factory in eerste instatie alleen informatie heeft over de formulier naam, breiden we deze uit met de instantie van het formulier, hierdoor kunnen ook de velden op het formulier bekeken worden. Vervolgens voegen we aan de factory toe dat als het formulier de audit velden bevat, dat deze ook de audit logica moet toepassen bij het opslaan.


## Terugblik
### Oud
        public Dictionary<string, object> GetPrefillData(string formName)
        {
            var preFillDictionary = new Dictionary<string, object>();
            var form = _formRepository.GetFormDefinition(formName);
            var user = _userContextProvider.GetCurrentAuthenticatedUser();

            if (form.FormName == AddressForm.FORM_NAME)
            {
                if (user != null)
                {
                    var (postalCode, houseNumber) = _addressProvider.GetPostalCodeAndHouseNumber(user.IpAddress);
                    AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.PostalCode), postalCode);
                    AddPrefillValueToDictionary(preFillDictionary, nameof(AddressForm.HouseNumber), houseNumber);
                }
            }
            else if(form.FormName == PersonalInformationForm.FORM_NAME)
            {
                if (user != null)
                {
                    AddPrefillValueToDictionary(preFillDictionary, nameof(PersonalInformationForm.Name), user.Name);
                }
            }

            return preFillDictionary;
        }

        public bool SubmitForm(IForm form)
        {
            var user = _userContextProvider.GetCurrentAuthenticatedUser();
            if (form.FormName == AddressForm.FORM_NAME)
            {
                var (province, street) = _addressProvider.GetProvinceAndStreet(form.SubmittedValues[nameof(AddressForm.PostalCode)]?.ToString(),
                    form.SubmittedValues[nameof(AddressForm.HouseNumber)]?.ToString());

                form.SubmittedValues[nameof(AddressForm.Province)] = province;
                form.SubmittedValues[nameof(AddressForm.Street)] = street;

                if (user != null)
                {
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserEmail)] = user.Email;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserId)] = user.Id;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserIpAddress)] = user.IpAddress;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserName)] = user.Name;
                }
            }
            else if (form.FormName == PersonalInformationForm.FORM_NAME)
            {
                if (user != null)
                {
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserEmail)] = user.Email;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserId)] = user.Id;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserIpAddress)] = user.IpAddress;
                    form.SubmittedValues[nameof(IAuditInformation.AuthenticatedUserName)] = user.Name;
                }
            }

            return _formRepository.Submit(form);
        }

### Nieuw
        public Dictionary<string, object> GetPrefillData(string formName)
        {
            var preFillDictionary = new Dictionary<string, object>();
            var form = _formRepository.GetFormDefinition(formName);

            var formLogicStrategy = GetFormLogicStrategy(form);
            formLogicStrategy.AddPrefillData(preFillDictionary);

            return preFillDictionary;
        }

        public bool SubmitForm(IForm form)
        {
            var formLogicStrategy = GetFormLogicStrategy(form);
            formLogicStrategy.AddOnPostData(form);

            return _formRepository.Submit(form);
        }
        
## Nieuwe wens
Als we kijken naar de controller zelf is dit een stuk overzichtelijke geworden, bedenk hoeveel werk het nu zou zijn om nog een formulier te maken, met een eigen pre-fill logica. En wat als bij het formulier ook opgeslagen moet worden wie de ingelogde gebruiker is.

Wat als het adres formulier een InsertedDate krijgt en alle formulieren met InsertedDate altijd de tijd van het opslaan moeten vullen?
