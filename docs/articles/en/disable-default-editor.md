# Disabling the Default Editor

By default, Alchemy uses its own editor class to handle the drawing of all types. However, to avoid conflicts with other libraries or assets, you can disable this behavior.

To disable the default editor, add `ALCHEMY_DISABLE_DEFAULT_EDITOR` to the `Scripting Define Symbols` field in `Project Settings > Player`. If you want to use Alchemy's features while this option is enabled, you'll need to define your own editor class that inherits from `AlchemyEditor`.