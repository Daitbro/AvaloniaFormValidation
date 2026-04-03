using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows.Input;
using AvaloniaFormValidation.Models;
using ReactiveUI;

namespace AvaloniaFormValidation.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "userdata.json");
    private Person _person;
    private string _firstNameError = string.Empty;
    private string _lastNameError = string.Empty;
    private string _genderError = string.Empty;
    private bool _isValid;

    public ObservableCollection<string> Genders { get; } = new() { "Мужской", "Женский" };

    public MainWindowViewModel()
    {
        _person = new Person();
        SaveCommand = ReactiveCommand.Create(SaveData);
        LoadData();
        
        this.WhenAnyValue(x => x.Person.FirstName).Subscribe(_ => ValidateFirstName());
        this.WhenAnyValue(x => x.Person.LastName).Subscribe(_ => ValidateLastName());
        this.WhenAnyValue(x => x.Person.Gender).Subscribe(_ => ValidateGender());
    }

    public Person Person
    {
        get => _person;
        set => this.RaiseAndSetIfChanged(ref _person, value);
    }

    public string FirstNameError
    {
        get => _firstNameError;
        set => this.RaiseAndSetIfChanged(ref _firstNameError, value);
    }

    public string LastNameError
    {
        get => _lastNameError;
        set => this.RaiseAndSetIfChanged(ref _lastNameError, value);
    }

    public string GenderError
    {
        get => _genderError;
        set => this.RaiseAndSetIfChanged(ref _genderError, value);
    }

    public bool IsValid
    {
        get => _isValid;
        set => this.RaiseAndSetIfChanged(ref _isValid, value);
    }

    public ICommand SaveCommand { get; }

    private void ValidateFirstName()
    {
        FirstNameError = string.IsNullOrWhiteSpace(Person.FirstName) 
            ? "Имя обязательно для заполнения" 
            : string.Empty;
        UpdateIsValid();
    }

    private void ValidateLastName()
    {
        LastNameError = string.IsNullOrWhiteSpace(Person.LastName) 
            ? "Фамилия обязательна для заполнения" 
            : string.Empty;
        UpdateIsValid();
    }

    private void ValidateGender()
    {
        GenderError = string.IsNullOrWhiteSpace(Person.Gender) 
            ? "Выберите пол" 
            : string.Empty;
        UpdateIsValid();
    }

    private void UpdateIsValid()
    {
        IsValid = string.IsNullOrEmpty(FirstNameError) &&
                  string.IsNullOrEmpty(LastNameError) &&
                  string.IsNullOrEmpty(GenderError);
    }

    private void SaveData()
    {
        ValidateFirstName();
        ValidateLastName();
        ValidateGender();

        if (!IsValid)
            return;

        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Person, options);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }

    private void LoadData()
    {
        if (!File.Exists(_filePath))
            return;

        try
        {
            string json = File.ReadAllText(_filePath);
            var loadedPerson = JsonSerializer.Deserialize<Person>(json);
            if (loadedPerson != null)
            {
                Person = loadedPerson;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }
}