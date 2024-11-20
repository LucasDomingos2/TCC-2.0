const express = require('express');
const app = express();
const port = 3000;

let cache = {
    questions: [
        {
            id: 1,
            question: "O que é uma variável?",
            answers: ["Um valor constante", "Um tipo de dado", "Um espaço na memória", "Uma função"],
            correctAnswer: "Um espaço na memória"
        },
        {
            id: 2,
            question: "Qual é o resultado de 5 % 2 em JavaScript?",
            answers: ["0", "1", "2", "3"],
            correctAnswer: "1"
        },
        {
            id: 3,
            question: "O que significa a palavra-chave 'let' em JavaScript?",
            answers: ["Declara uma variável com escopo de função", "Declara uma variável com escopo de bloco", "Declara uma constante", "Declara uma função"],
            correctAnswer: "Declara uma variável com escopo de bloco"
        },
        {
            id: 4,
            question: "Qual é o principal objetivo de um loop 'for'?",
            answers: ["Declarar uma variável", "Executar código repetidamente", "Testar condições", "Criar funções"],
            correctAnswer: "Executar código repetidamente"
        },
        {
            id: 5,
            question: "O que o operador '===' faz em JavaScript?",
            answers: ["Compara apenas valores", "Compara valores e tipos", "Associa um valor a uma variável", "Adiciona elementos a um array"],
            correctAnswer: "Compara valores e tipos"
        },
        {
            id: 6,
            question: "Como você declara uma constante em JavaScript?",
            answers: ["let", "var", "const", "define"],
            correctAnswer: "const"
        }
    ],
    playerStats: {
        health: 5,
        score: 0
    }
};

app.use(express.json());

// Endpoint para obter as perguntas
app.get('/api/questions', (req, res) => {
    res.json(cache.questions);
});

// Endpoint para verificar a resposta de uma pergunta
app.post('/api/check-answer', (req, res) => {
    const { questionId, answer } = req.body;
    const question = cache.questions.find(q => q.id === questionId);

    if (!question) {
        return res.status(404).json({ message: 'Pergunta não encontrada' });
    }

    let correct = (question.correctAnswer === answer);

    if (correct) {
        cache.playerStats.score += 10; 
    } else {
        cache.playerStats.health -= 1; 
        triggerEnemyAttack();
    }

    res.json({
        correct,
        playerStats: cache.playerStats
    });
});

// Endpoint para atualizar as perguntas (pelo botão na Unity)
app.post('/api/update-questions', (req, res) => {
    cache.questions = [
        {
            id: 1,
            question: "Qual é a principal função de um algoritmo?",
            answers: ["Resolver problemas", "Repetir código", "Testar condições", "Definir variáveis"],
            correctAnswer: "Resolver problemas"
        },
        {
            id: 2,
            question: "Qual é a estrutura correta para declarar uma função em JavaScript?",
            answers: ["function nomeFuncao()", "let nomeFuncao = () => {}", "const nomeFuncao = function() {}", "Todas estão corretas"],
            correctAnswer: "Todas estão corretas"
        },
        {
            id: 3,
            question: "Qual é o propósito do 'return' em uma função?",
            answers: ["Declarar uma variável", "Encerrar o programa", "Retornar um valor da função", "Chamar outra função"],
            correctAnswer: "Retornar um valor da função"
        },
        {
            id: 4,
            question: "O que é um array?",
            answers: ["Um tipo de loop", "Um conjunto de valores", "Um operador lógico", "Uma função"],
            correctAnswer: "Um conjunto de valores"
        },
        {
            id: 5,
            question: "Qual é a saída do código: console.log(typeof [])?",
            answers: ["array", "object", "undefined", "function"],
            correctAnswer: "object"
        },
        {
            id: 6,
            question: "Como você pode iterar sobre um array em JavaScript?",
            answers: ["for", "map", "forEach", "Todas estão corretas"],
            correctAnswer: "Todas estão corretas"
        }
    ];

    res.json({ message: "Perguntas atualizadas com sucesso!" });
});

// Função auxiliar para simular o ataque do inimigo
function triggerEnemyAttack() {
    console.log('O inimigo está atacando!');
}

app.listen(port, () => {
    console.log(`API rodando na porta ${port}`);
});
