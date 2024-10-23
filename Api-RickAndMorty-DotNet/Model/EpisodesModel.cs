﻿namespace Api_RickAndMorty_DotNet.Model;

public class EpisodesModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime air_date { get; set; }
    public string? Episode {  get; set; }
    public List<string>? Characters { get; set; }
}