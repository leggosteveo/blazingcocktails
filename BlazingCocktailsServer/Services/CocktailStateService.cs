using BlazingCocktailsServer;
using Microsoft.EntityFrameworkCore;



public class CocktailStateService


{
    public event Action? OnChange;

    private readonly ApplicationDbContext _context;
    private List<Cocktail>? _cocktailList;
    private Cocktail? _selectedCocktail;
    public CocktailStateService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<List<Cocktail>> GetAllCocktailsAsync()
    {
        if (_cocktailList == null)
        {
            await LoadCocktailsAsync();
        }
        return _cocktailList!;
    }

    
    public async Task<Cocktail?> GetSingleCocktailAsync(int Id)
    {
        if (_cocktailList == null)
        {
            await LoadCocktailsAsync();
        }
        return _cocktailList?.FirstOrDefault(x => x.Id == Id);
    }
    private async Task LoadCocktailsAsync()
    {
        try
        {
            _cocktailList = await _context.Cocktails.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading liquors: {ex.Message}");
            _cocktailList = new List<Cocktail>();
        }
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();



}

