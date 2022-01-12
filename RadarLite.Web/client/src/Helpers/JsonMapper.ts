/*eslint-disable @typescript-eslint/no-use-before-define */

import "reflect-metadata";
const JSON_META_DATA_KEY = "JsonProperty";

//eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
export function isTargetType(val: any, type: "object" | "string"): boolean {
  return typeof val === type;
}
//eslint-disable-next-line @typescript-eslint/explicit-module-boundary-types
export function isPrimitiveOrPrimitiveClass(obj: any): boolean {
  return !!(
    ["string", "boolean", "number"].indexOf(typeof obj) > -1 ||
    obj instanceof String ||
    obj === String ||
    obj instanceof Number ||
    obj === Number ||
    obj instanceof Boolean ||
    obj === Boolean
  );
}
// eslint-disable-next-line @typescript-eslint/ban-types
export function isArrayOrArrayClass(clazz: Function): boolean {
  if (clazz === Array) {
    return true;
  }
  return Object.prototype.toString.call(clazz) === "[object Array]";
}
/**
 * provide interface to indicate the object is allowed to be traversed
 *
 * @interface
 */
// export interface IGenericObject {
//   [key: string]: any;
// }

// /**
//  * Decorator variable name
//  *
//  * @const
//  */
// const JSON_META_DATA_KEY = "JsonProperty";

// /**
//  * When custom mapping of a property is required.
//  *
//  * @interface
//  */
// export interface ICustomConverter {
//   fromJson(data: any): any;
//   toJson(data: any): any;
// }

// /**
//  * IDecoratorMetaData<T>
//  * DecoratorConstraint
//  *
//  * @interface
//  * @property {ICustomConverter} customConverter, will be used for mapping the property, if specified
//  * @property {boolean} excludeToJson, will exclude the property for serialization, if true
//  */
// export interface IDecoratorMetaData<T> {
//   name?: string;
//   clazz?: { new (): T };
//   customConverter?: ICustomConverter;
//   excludeToJson?: boolean;
// }

/**
 * DecoratorMetaData
 * Model used for decoration parameters
 *
 * @class
 * @property {string} name, indicate which json property needed to map
 * @property {string} clazz, if the target is not primitive type, map it to corresponding class
 */
class DecoratorMetaData<T> implements JsonMapper.IDecoratorMetaData<T> {
  constructor(public name?: string, public Clazz?: { new (): T }) {}
}

/**
 * JsonProperty
 *
 * @function
 * @property {IDecoratorMetaData<T>|string} metadata, encapsulate it to DecoratorMetaData for standard use
 * @return {(target:Object, targetKey:string | symbol)=> void} decorator function
 */
export function JsonProperty<T>(
  metadata?: JsonMapper.IDecoratorMetaData<T> | string
): (target: Record<string, any>, targetKey: string | symbol) => void {
  let decoratorMetaData: JsonMapper.IDecoratorMetaData<T>;

  if (isTargetType(metadata, "string")) {
    decoratorMetaData = new DecoratorMetaData<T>(metadata as string);
  } else if (isTargetType(metadata, "object")) {
    decoratorMetaData = metadata as JsonMapper.IDecoratorMetaData<T>;
  } else {
    throw new Error(
      `index.ts: meta data in Json property is undefined. meta data: ${metadata}`
    );
  }

  return Reflect.metadata(JSON_META_DATA_KEY, decoratorMetaData);
}

/**
 * getClazz
 *
 * @function
 * @property {any} target object
 * @property {string} propertyKey, used as target property
 * @return {Function} Function/Class indicate the target property type
 * @description Used for type checking, if it is not primitive type, loop inside recursively
 */
function getClazz<T>(target: T, propertyKey: string): { new (): T } {
  return Reflect.getMetadata("design:type", target, propertyKey);
}

/**
 * getJsonProperty
 *
 * @function
 * @property {any} target object
 * @property {string} propertyKey, used as target property
 * @return {IDecoratorMetaData<T>} Obtain target property decorator meta data
 */
function getJsonProperty<T>(
  target: any,
  propertyKey: string
): JsonMapper.IDecoratorMetaData<T> {
  return Reflect.getMetadata(
    JSON_META_DATA_KEY,
    target,
    propertyKey
  ) as JsonMapper.IDecoratorMetaData<T>;
}

/**
 * hasAnyNullOrUndefined
 *
 * @function
 * @property {...args:any[]} any arguments
 * @return {IDecoratorMetaData<T>} check if any arguments is null or undefined
 */
function hasAnyNullOrUndefined(...args: any[]) {
  return args.some((arg: any) => arg === null || arg === undefined);
}

function mapFromJson<T>(
  decoratorMetadata: JsonMapper.IDecoratorMetaData<T>,
  instance: T,
  json: JsonMapper.IGenericObject,
  key: string
): any {
  //if decorator name is not found, use target property key as decorator name. It means mapping it directly
  const decoratorName: string = decoratorMetadata.Name || key;
  const innerJson: any = json ? json[decoratorName] : undefined;

  if (!isPrimitiveOrPrimitiveClass(innerJson)) {
    const metadata: JsonMapper.IDecoratorMetaData<T> = getJsonProperty(
      instance,
      key
    );

    const reflectedTypeClazz = getClazz(instance, key);
    const clazz =
      metadata && metadata.Clazz ? metadata.Clazz : reflectedTypeClazz;

    if (clazz !== undefined) {
      if (isArrayOrArrayClass(reflectedTypeClazz)) {
        if (
          (metadata && metadata.Clazz) ||
          isPrimitiveOrPrimitiveClass(clazz)
        ) {
          if (
            innerJson &&
            isArrayOrArrayClass(innerJson) &&
            metadata.Clazz !== undefined
          ) {
            const clazz = metadata.Clazz;
            return innerJson.map((item: any) => deserialize(clazz, item));
          }

          return undefined;
        } else {
          return innerJson;
        }
      }
      if (!isPrimitiveOrPrimitiveClass(clazz)) {
        return NullableDeserialize(clazz, innerJson);
      }
    }
  }
  return innerJson;
}

function NullableDeserialize<T extends JsonMapper.IGenericObject>(
  clazz: { new (): T },
  json: JsonMapper.IGenericObject
): T | null {
  if (hasAnyNullOrUndefined(clazz, json)) {
    return null;
  }
  if (!isTargetType(json, "object")) {
    return null;
  }
  return InternalDeserialize(clazz, json);
}
/**
 * deserialize
 *
 * @function
 * @param {{new():T}} clazz, class type which is going to initialize and hold a mapping json
 * @param {Object} json, input json object which to be mapped
 *
 * @return {T} return mapped object
 */
export function deserialize<T extends JsonMapper.IGenericObject>(
  Clazz: { new (): T },
  json: JsonMapper.IGenericObject
): T {
  /**
   * As it is a recursive function, ignore any arguments that are unset
   */
  if (hasAnyNullOrUndefined(Clazz, json)) {
    throw new Error("Deserialize in JSON");
  }

  if (!isTargetType(json, "object")) {
    throw new Error("Deserialize in JSON");
  }
  return InternalDeserialize(Clazz, json);
}

function InternalDeserialize<T extends Record<string, any>>(
  clazz: { new (): T },
  json: JsonMapper.IGenericObject
): T {
  const instance = new clazz();
  Object.keys(instance).forEach((key: keyof T) => {
    const decoratorMetaData: JsonMapper.IDecoratorMetaData<
      Record<string, unknown>
    > = getJsonProperty(instance, key as string);

    if (decoratorMetaData) {
      if (decoratorMetaData.CustomConverter) {
        instance[key] = decoratorMetaData.CustomConverter.FromJson(
          json[decoratorMetaData.Name || (key as string)]
        ) as T[keyof T];
      } else if (decoratorMetaData.ExcludeFromJson !== true) {
        instance[key] = mapFromJson(
          decoratorMetaData,
          instance,
          json,
          key as string
        );
      }
    } else {
      instance[key] = json[key as string];
    }
  });

  return instance;
}

export function DeserializeArray<T extends JsonMapper.IGenericObject>(
  clazz: { new (): T },
  json: JsonMapper.IGenericObject[]
): T[] {
  const arr: T[] = [];
  for (let i = 0; i < json.length; i++) {
    const item: T | null = NullableDeserialize(clazz, json[i]); //changed to address issue with object
    if (item !== null) {
      arr.push(item);
    }
  }
  return arr;
}
/**
 * Serialize: Creates a ready-for-json-serialization object from the provided model instance.
 * Only @JsonProperty decorated properties in the model instance are processed.
 *
 * @param instance an instance of a model class
 * @returns {any} an object ready to be serialized to JSON
 */
export function serialize(instance: any): any {
  if (!isTargetType(instance, "object") || isArrayOrArrayClass(instance)) {
    return instance;
  }

  if (instance === null) {
    return null;
  }

  const obj: any = {};
  Object.keys(instance).forEach((key) => {
    const metadata: JsonMapper.IDecoratorMetaData<Record<string, any>> =
      getJsonProperty(instance, key);
    obj[metadata && metadata.Name ? metadata.Name : key] = serializeProperty(
      metadata,
      instance[key]
    );
  });
  return obj;
}

/**
 * Prepare a single property to be serialized to JSON.
 *
 * @param metadata
 * @param prop
 * @returns {any}
 */
function serializeProperty(
  metadata: JsonMapper.IDecoratorMetaData<any>,
  prop: any
): any {
  if (metadata) {
    if (metadata.ExclueToJson) {
      return undefined;
    }
    if (metadata.CustomConverter) {
      return metadata.CustomConverter.ToJson(prop);
    }
    if (!metadata.Clazz) {
      return prop;
    }
    if (isArrayOrArrayClass(prop)) {
      return prop.map((propItem: any) => serialize(propItem));
    }
    return serialize(prop);
  } else {
    return undefined;
  }
}
