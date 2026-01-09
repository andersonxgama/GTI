using System.Collections.Generic;
using System.ServiceModel;
using WCFServiceHost;

[ServiceContract]
public interface IClienteService
{
    [OperationContract]
    void Inserir(Cliente cliente);

    [OperationContract]
    void Atualizar(Cliente cliente);

    [OperationContract]
    void Excluir(int id);

    [OperationContract]
    List<Cliente> Listar();
}
