declare namespace JsonMapper {
  interface IGenericObject {
    [key: string]: any;
  }

  interface ICustomConverter<T> {
    FromJson(data: string);

    ToJson(data: T): Nullable<string>;
  }
  interface IDecoratorMetaData<T> {
    Clazz?: { new (): T };
    CustomConverter?: ICustomConverter<T>;
    ExcludeFromJson?: boolean;
    ExclueToJson?: boolean;
    Name?: string;
  }
}
