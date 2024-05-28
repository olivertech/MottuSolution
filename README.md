*Descrição do projeto*

O presente projeto visa apresentar uma abordagem mais ampla de conhecimentos para montagem de uma solução completa em aspnet core, cobrindo toda a parte backend do projeto, usando outras soluções e tecnologias em apoio.

Esse projeto foi desenvolvido para a empresa Mottu, com objetivo de apresentar uma proposta moderna de arquitetura de sistemas, usando boas práticas para servir de solução para sistemas que vinham apresentando problemas internos arquiteturais, sem boas práticas e com dependências fortes (altíssimo acoplamento entre projetos e camadas), causando problemas na manutenção corretiva e até mesmo evolutiva.

A solução desenvolvida aqui segue o conceito arquitetural conhecido como "Clean Architecture", utilizado em sistemas com arquiteturas modernas, permitindo maior facilidade para manutenção e expansão, e melhor entendimento do código, com divisão clara de responsabilidades para cada uma das camadas que compõem a solução.

A solução está dividida nas seguintes camadas/projetos:

- Projeto Domain e Projeto Application - Contém as regras de negócio
- Projeto Infrastructure - Contém a persistência de dados
- Projeto CrossCutting - Contém as classes referenciadas pelos demais projetos
- Projeto User Interface - Contém a apresentação/Api
- Projeto Consumer - Contém a lógica associada ao consumer da mensageria, usado apenas para fins de teste do RabbitMQ
- Projeto UnitTest - Contém apenas algumas classes de testes, utilizando a ferramenta XUnit.Net

Alguns dos patterns usados no projeto:

- Clean Architecture
- n-Tier
- Unit Of Work
- Repository
- Class Mapping
- ORM
 
O projeto utiliza mensageria por meio do RabbitMQ em conjunto com o MassTransit, rodando por meio de containers.

Para o banco de dados, foi utilizado o Postgrees versão 16.2, implementando a tecnologia ORM com Entity Framework Core, para abstração do banco de dados. Além disso, por meio do EF Core, foi adotado a abordagem "Code First" para modelagem de banco de dados, com uso de Migrations pra montagem das entidades do banco.

A seguir, a modelagem proposta do banco de dados, de acordo com funcionalidades apresentadas para esse sistema fictício de gestão de locações de moto.

![image](https://github.com/olivertech/MottuSolution/assets/6912641/65ecc7e8-85a7-4d4e-9ead-7343eca60c3b)

Para dar suporte a execução da solução, é preciso ter instalado o Docker localmente. A solução possui um Docker-compose.yml que levanta todos os containers. Serão criados os seguintes containers que irão dar suporte na execução do sistema:

- Container do banco de dados Postgrees
- Container do RabbitMQ
- Container do Redis
- Container do MongoDB

![image](https://github.com/olivertech/MottuSolution/assets/6912641/66df8f4b-c42c-4fdf-9aee-d4a38fef85bd)

Com a presente solução, se propõe mostrar uma meríade de tecnologias sendo utilizadas num projeto aspnet core, de forma a contemplar boas práticas e a adoção de convenções e padrões de mercado, aplicados ao desenvolvimento de sistemas, afim de ajudar a solucionar problemas existentes e enfrentados pela empresa Mottu.

Na raiz da solução encontra-se um pdf que abrange mais detalhes do projeto e como instalar e rodar o mesmo localmente.

Obs: O projeto segue sendo evoluído. No presente momento, segue sendo implementado o banco de dados NoSql MongoDB.

Para maiores informações, ou contato profissional, me coloco a disposição pelo email olivertech@outlook.com, pelo celular (21) 99710-8994 (Ligações / WhatsApp) ou pelo Skype maclauservicos.

Marcelo de Oliveira.
