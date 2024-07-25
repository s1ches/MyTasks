SELECT
    p.[Name] AS product,
    c.[Name] AS category
FROM [Products] p
LEFT JOIN [ProductsCategories] pc ON p.[Id] = pc.[ProductId]
LEFT JOIN [Categories] c ON c.[Id] = pc.[CategoryId];
