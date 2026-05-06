namespace CP2_CS.Application.Interfaces;

public interface IMessagePublisher
{
    void Publicar(string fila, string mensagem);
}
