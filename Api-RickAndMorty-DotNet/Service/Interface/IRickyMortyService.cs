﻿namespace Api_RickAndMorty_DotNet.Service.Interface;

public interface IRickyMortyService
{
    Task<string> GetRickyMortyRandom();
    Task<string> GetRickyMortyById(int id); 

}
