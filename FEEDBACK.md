# Feedback - Avaliação Geral

## Front End
### Navegação
* **Pontos Positivos:**
  - Será avaliado na entrega final

### Design
* **Pontos Positivos:**
  - Será avaliado na entrega final

### Funcionalidade
* **Pontos Positivos:**
  - Funcionalidades essenciais de CRUD para produtos e categorias implementadas.
  - Login e autenticação via Cookie funcionando no MVC.

## Back End
### Arquitetura
* **Pontos Positivos:**
  - Projeto tem separação física entre MVC (`App`) e API (`Api`), e estrutura funcional no geral.

* **Pontos Negativos:**
  - A arquitetura apresenta **complexidade desnecessária e além do escopo do módulo**:
    - `Application`, `Business`, `Data`, `IoC`, `Shared`, etc.
    - O uso de múltiplos projetos e abstrações (como repositórios, serviços, IoC separado) não é compatível com a proposta de um CRUD simples.
  - Para esse módulo, seria suficiente agrupar as regras e persistência na camada `Core`, mantendo a solução enxuta.
  - A estrutura atual aumenta o custo cognitivo.

  - Como foi orientado, guarde seu arsenal técnico para o momento certo, "matar uma formiga com um canhão" é visto como uma má prática.

### Funcionalidade
* **Pontos Positivos:**
  - Endpoints da API implementam autenticação JWT corretamente.

* **Pontos Negativos:**
  - A API não bloqueia alterações entre usuários (qualquer usuário autenticado pode editar/remover produto de outro).

### Modelagem
* **Pontos Positivos:**
  - Entidades principais estão presentes e modeladas: Produto, Categoria e Vendedor.
  - Relacionamento entre Produto e Identity (via ID compartilhado) respeita o escopo.

* **Pontos Negativos:**
  - Modelagem excessivamente técnica para o nível proposto, com interfaces, heranças e abstrações além do necessário.
  - Poderia ser reduzido e mais direto, centralizando validações e lógica básica na camada Core de forma simplificada.

## Projeto
### Organização
* **Pontos Positivos:**
  - Estrutura geral bem dividida em `src/`, com soluções e projetos bem nomeados.

* **Pontos Negativos:**
  - O excesso de camadas e separações prejudica a clareza para um projeto introdutório.
  - A divisão entre `Business`, `Application`, `Data`, `Shared`, `IoC` só faria sentido em um projeto mais avançado (ex: Módulo 3+).

### Documentação
* **Pontos Positivos:**
  - `README.md` apresenta boa estrutura: descrição, execução, tecnologias e como rodar.

### Instalação
* **Pontos Positivos:**
  - Projeto funciona com SQLite em modo de desenvolvimento.
  - Seed de dados incluído, facilitando testes.
