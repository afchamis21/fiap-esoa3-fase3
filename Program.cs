using Fiap.Agnello.CLI.Application;
using Fiap.Agnello.CLI.Application.Repository;

#region repos
Repositories.RegisterRepository(() => new WineDbRepository());
Repositories.RegisterRepository(() => new BrandDbReposiory());
#endregion

Application.Execute();
