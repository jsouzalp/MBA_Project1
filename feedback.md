
# Feedback do Instrutor

#### 28/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Criou corretamente o Autor correlacionado ao User (mas apenas na camada Web, não na API)
- Mostrou entendimento do ecossistema de desenvolvimento em .NET

## Pontos Negativos:

- A arquitetura está extremamente complexa para o tamanho do desafio, não existe necessidade das deciões tomadas.
- Apesar da complexidade e recursos não solicitados no escopo alguns pontos obrigatórios e relativamente simples não foram atendidos
    - Validar se o usuário é dono do post ou admin ao modificar
    - Implementar o identity na API com JWT    

## Sugestões:

- Implementar as necessidades descritas no escopo
- Guarde a munição da arquitetura complexa para projetos complexos.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
