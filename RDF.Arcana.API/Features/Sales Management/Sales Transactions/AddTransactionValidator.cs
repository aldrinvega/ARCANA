using FluentValidation;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions
{
    public class AddTransactionValidator : AbstractValidator<AddTransaction.AddtransactionCommand>
    {
        public AddTransactionValidator()
        {
            //RuleFor(c => c.ClientId)
            //    .Empty().WithMessage("Client Id must be null");

            //RuleForEach(t => t.Items)
            //    .ChildRules(i =>
            //    {
            //        i.RuleFor(i => i.ItemId)
            //        .Empty().WithMessage("ItemId must be null");
            //    });
                    
                
        }
    }
    
}
