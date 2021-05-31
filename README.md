# e-construmarket-mvc

![Imagem de demonstração da aplicação](e_construmarket_mvc_demo.jpg)

# Para testar o projeto:
1. Clone o repositório em um diretório local;
2. Mude o diretório atual até chegar no diretório do projeto (onde está o arquivo `.csproj`); e
3. Execute o comando `dotnet run` (ou clique em IIS Express se estiver usando o Visual Studio).
4. Ao visualizar a interface principal, haverá um elemento de `input` para pesquisa de produtos. Os possíveis produtos são: 
**mouse, teclado e fones de ouvido**.

Qualquer `substring` dentro dos nomes dos produtos será encontrada também. Por exemplo, se procurar por "mecanico" ou "teclado mecanico", o resultado ainda trará os produtos associados à pesquisa.

# Informações importantes

Para testar seu comportamento principal, é importante que a aplicação e-construmarket (WebAPI) esteja em execução. 

Basta fazer seu `git clone` atráves [deste link](https://github.com/JustAn0therDev/e-construmarketWebAPI.git).
