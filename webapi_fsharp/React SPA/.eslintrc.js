module.exports = {
	extends: [
		'react-app', // Create React App base settings
		'plugin:prettier/recommended', // Enables eslint-plugin-prettier and eslint-config-prettier. This will display Prettier errors as ESLint errors. Make sure this is always the last configuration in the extends array.
	],
	rules: {
		'prettier/prettier': [
			'error',
			{
				printWidth: 140,
				singleQuote: true,
				tabWidth: 4,
				useTabs: true,
				endOfLine: 'auto',
				jsxBracketSameLine: true,
				bracketSpacing: true,
			},
		],
		// "@typescript-eslint/no-empty-function": ["error", { "allow": ["arrowFunctions"] }],
		// "@typescript-eslint/explicit-function-return-type": ["error", {
		// 	"allowExpressions": true,
		// 	// "allowTypedFunctionExpressions": true,
		// 	// "allowConciseArrowFunctionExpressionsStartingWithVoid": true
		// }],
		// "@typescript-eslint/no-unused-vars": ["error", { "argsIgnorePattern": "^_", "varsIgnorePattern": "^_" }],
	},
};
