using WebApp.Enums;
using WebApp.Models;
using WebApp.Wrappers;

namespace WebApp.Components.Pages
{
    public partial class Home
    {
        private string _meatType = "0";
        private string _priceType = "0";
        private string _vegeType = "0";
        private string _sosType = "0";
        private List<ModelResult>? _results;
        
        private void Calculate()
        {
            using (var wrapper = new ModelWrapper())
            {
                wrapper.LoadModel();
                wrapper.SetSosType((SosType)(int.TryParse(_sosType, out var sosType) ? sosType : 0));
                wrapper.SetMeatType((MeatType)(int.TryParse(_meatType, out var meatType) ? meatType : 0));
                wrapper.SetPriceType((PriceType)(int.TryParse(_priceType, out var priceType) ? priceType : 0));
                wrapper.SetVegetableType((VegetableType)(int.TryParse(_vegeType, out var vegeType) ? vegeType : 0));
                
                _results = wrapper.GetResults();
            }
        }

        private void EditSearch()
        {
            _results = null;
        }

        private void NewSearch()
        {
            _meatType = "0";
            _vegeType = "0";
            _sosType = "0";
            _priceType = "0";
            _results = null;
        }
    }
}