Combinations Tool
 v. 2021.07.17.1
de LWMolin

Caso de Uso Hipotético

Suponha que a empresa X possa emitir relatórios de vendas agrupadas por categoria apenas com o valor total a nível de dia. 
No relatório emitido, consta que para o dia 15 de janeiro de 1980, o valor total das vendas para a categoria Alfa fora igual a R$ 1463. 
Na consulta ao banco de dados, para todas vendas daquele dia, você obteve o seguinte resultado:

![image](https://user-images.githubusercontent.com/28737900/131934873-19e0e49d-f628-40c4-9a4e-94f2c3409e9e.png)

A tabela contém o código do produto e o valor da venda, porém a categoria a qual cada produto pertence não está no banco de dados; é uma regra interna da empresa que desenvolveu o ERP da empresa X. Então, como você irá descobrir quais produtos pertencem à categoria Alfa? 

Uma solução rápida é a Combinations Tool, na qual basta informar o conjunto de valores, que neste caso é:

214;173;302;83;189;514;279;649;786;124;732;131;214;41;241;289

O conjunto de códigos de produto:

B5;D4;C3;A9;D4;F7;C3;F7;F7;B5;D4;B5;F7;A9;D4;C3

E, por fim, o valor total de vendas da categoria Alfa:

1463

Ao clicar no botão Calcular, a mágica acontece:

![image](https://user-images.githubusercontent.com/28737900/131934848-5b60bf8c-7dff-46f9-8de3-fe07edeb64ce.png)

O programa mostrará todas as combinações encontradas cujo somatório seja igual ao valor total informado. 

No exemplo em questão, foram encontradas 7 combinações. Como saber qual delas é a combinação correta? Como o objetivo é encontrar quais produtos pertencem à categoria Alfa, na combinação correta um produto dessa categoria sempre estará entre os valores incluídos (In) e nunca entre os valores não-incluídos (Out). Por exemplo, na primeira combinação encontrada:

In: 302 (C3); 83 (A9); 279 (C3); 124 (B5); 131 (B5), 214 (F7), 41 (A9), 289 (C3)
Out: 214 (B5); 173 (D4); 189 (D4); 514 (F7); 649 (F7); 786 (F7); 732 (D4); 241 (D4)

O produto de código F7 aparece tanto na lista de valores incluídos quanto na lista de valores não-incluídos, logo esse produto não faz parte da categoria Alfa.

Na terceira combinação encontrada, os produtos que aparecem na lista de valores incluídos nunca aparecem na lista de valores não-incluídos e vice-versa. Logo, para o exemplo em questão, pode-se concluir que a categoria Alfa provavelmente é composta pelos produtos de código A9, B5 e C3.

In: 214 (B5); 302 (C3); 83 (A9); 279 (C3); 124 (B5); 131 (B5); 41 (A9); 289 (C3)
Out: 173 (D4); 189 (D4); 514 (F7); 649 (F7); 786 (F7); 732 (D4); 214 (F7); 241 (D4)

O programa também mostra a quantidade total de combinações verificadas, o tempo de execução estimado (CE), a quantidade de resultados encontrados, o tempo total de execução (T), o tempo gasto para o processamento de todas as combinações (C) e o tempo gasto para a escrita do resultado (E).

Para download do arquivo executável, acesse a URL abaixo:

https://lucaswmolin.com/access/destiny.php?n=838c8c4c585f40348353c97a574caa9e
