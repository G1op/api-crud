using Cadastro.Data;
using Cadastro.Models;
using Microsoft.EntityFrameworkCore;

namespace Cd.Routes;

public static class CdRoute
{
    public static async void CdRoutes(this WebApplication app)
    {
        var route = app.MapGroup(prefix: "cadastro");
        route.MapPost(pattern: "", async(CadastroRequest req, CadastroContext context) =>
        {
            var cad = new CadastroModel(req.name);
            await context.AddAsync(cad);
            await context.SaveChangesAsync();
        });

        route.MapGet(pattern: "", async (CadastroContext context) =>
        {
            List<CadastroModel> cad = await context.CadastroM.ToListAsync();
            return Results.Ok(cad);
        });

        route.MapPut(pattern:"{id:guid}", async(Guid id, CadastroRequest req, CadastroContext context) =>
        {
            var cad = await context.CadastroM.FirstOrDefaultAsync(X => X.Id == id);
        
        
        if (cad == null)
            return Results.NotFound();

        cad.ChangeName(req.name);
        await context.SaveChangesAsync();

        return Results.Ok(cad);
        });

        route.MapDelete("{id:guid}", async(Guid id, CadastroContext context) =>
        {
            var cad = await context.CadastroM.FirstOrDefaultAsync(X => X.Id == id);

            if (cad == null)
                return Results.NotFound();

            cad.SetInactive();
            await context.SaveChangesAsync();

            return Results.Ok(cad);
        });

    }
}