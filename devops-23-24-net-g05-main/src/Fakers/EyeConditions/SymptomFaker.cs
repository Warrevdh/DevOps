using Domain.EyeConditions;
using System;


namespace Fakers.EyeConditions;

public class SymptomFaker : EntityFaker<Symptom>
{

    public SymptomFaker(string locale = "nl") : base(locale) 
    {
        //CustomInstantiator(f => new Symptom(f.PickRandom(f.Commerce.Categories(10))));
    }

}

