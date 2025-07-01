# APIProcessoSeletivo

## Passos para rodar o projeto

1. Clone o repositório e acesse a pasta do projeto:
   ```bash
   cd APIProcessoSeletivo/
   ```

2. Troque para a branch de desenvolvimento:
   ```bash
   git checkout develop
   ```

3. Suba os containers Docker (com build):
   ```bash
   docker compose up -d --build
   ```

4. Acesse a documentação da API (Swagger):
   [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)
5. Conecte-se com o banco 
  
      ```bash
    Nome do servidor: localhost,1433
    Usuário padrão: sa
    Senha: Admin123@
   ```
