using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "your_json_file_name");
// Firebase Admin SDK'y� yap�land�r�n
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault()
});

// Servisleri ekleyin
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware yap�land�rmas�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

