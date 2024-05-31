using Domain.EyeConditions;

namespace Fakers.EyeConditions;

public class EyeConditionFaker : EntityFaker<EyeCondition>
{
	public EyeConditionFaker(string locale = "nl") : base(locale)
	{
		CustomInstantiator(f => new EyeCondition(f.Commerce.ProductName(), f.Commerce.ProductMaterial(), "body",f.Image.PicsumUrl(), f.Image.PicsumUrl()));
	}
}