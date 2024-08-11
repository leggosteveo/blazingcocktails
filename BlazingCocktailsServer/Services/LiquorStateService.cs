using Microsoft.EntityFrameworkCore;
using BlazingCocktailsServer;



public class LiquorStateService
{
    public event Action? OnChange;

    private readonly ApplicationDbContext _context;
    private List<Liquor>? _liquorList;
    private Liquor? _selectedLiquor;

    public LiquorStateService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Liquor? SelectedLiquor
    {
        get => _selectedLiquor;
        set
        {
            _selectedLiquor = value;
            NotifyStateChanged();
        }
    }

    public async Task<List<Liquor>> GetAllLiquorsAsync()
    {
        if (_liquorList == null)
        {
            await LoadLiquorsAsync();
        }
        return _liquorList!;
    }

    public async Task<Liquor?> GetSingleLiquorAsync(string Name)
    {
        if (_liquorList == null)
        {
            await LoadLiquorsAsync();
        }
        return _liquorList?.FirstOrDefault(x => x.Name == Name);
    }

    private async Task LoadLiquorsAsync()
    {
        try
        {
            _liquorList = await _context.Liquors.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading liquors: {ex.Message}");
            _liquorList = new List<Liquor>();
        }
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

