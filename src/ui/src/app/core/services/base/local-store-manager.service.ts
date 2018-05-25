import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

import { Utilities } from './utilities';

@Injectable()
export class LocalStoreManager {

    public static syncListenerInitialised = false;
    public syncKeys: string[] = [];
    public initEvent = new Subject();

    public reservedKeys: string[] = [
        'sync_keys', 
        'addToSyncKeys', 
        'removeFromSyncKeys',
        'getSessionStorage', 
        'setSessionStorage', 
        'addToSessionStorage', 
        'removeFromSessionStorage', 
        'clearAllSessionsStorage'
    ];

    public static readonly DBKEY_USER_DATA = "user_data";
    public static readonly DBKEY_SYNC_KEYS = "sync_keys";

    public initialiseStorageSyncListener() {
        if (LocalStoreManager.syncListenerInitialised == true)
            return;

        LocalStoreManager.syncListenerInitialised = true;
        window.addEventListener("storage", this.sessionStorageTransferHandler, false);
        this.syncSessionStorage();
    }

    public deinitialiseStorageSyncListener() {

        window.removeEventListener("storage", this.sessionStorageTransferHandler, false);

        LocalStoreManager.syncListenerInitialised = false;
    }

    public sessionStorageTransferHandler = (event: StorageEvent) => {

        if (!event.newValue)
            return;

        if (event.key == 'getSessionStorage') {

            if (sessionStorage.length) {
                this.localStorageSetItem('setSessionStorage', sessionStorage);
                localStorage.removeItem('setSessionStorage');
            }
        }
        else if (event.key == 'setSessionStorage') {

            if (!this.syncKeys.length)
                this.loadSyncKeys();

            let data = JSON.parse(event.newValue);
            //console.info("Set => Key: Transfer setSessionStorage" + ",  data: " + JSON.stringify(data));

            for (let key in data) {

                if (this.syncKeysContains(key))
                    this.sessionStorageSetItem(key, JSON.parse(data[key]));
            }

            this.onInit();
        }
        else if (event.key == 'addToSessionStorage') {

            let data = JSON.parse(event.newValue);

            //console.warn("Set => Key: Transfer addToSessionStorage" + ",  data: " + JSON.stringify(data));

            this.addToSessionStorageHelper(data["data"], data["key"]);
        }
        else if (event.key == 'removeFromSessionStorage') {

            this.removeFromSessionStorageHelper(event.newValue);
        }
        else if (event.key == 'clearAllSessionsStorage' && sessionStorage.length) {

            this.clearInstanceSessionStorage();
        }
        else if (event.key == 'addToSyncKeys') {

            this.addToSyncKeysHelper(event.newValue);
        }
        else if (event.key == 'removeFromSyncKeys') {

            this.removeFromSyncKeysHelper(event.newValue);
        }
    }

    public syncSessionStorage() {

        localStorage.setItem('getSessionStorage', '_dummy');
        localStorage.removeItem('getSessionStorage');
    }

    public clearAllStorage() {

        this.clearAllSessionsStorage();
        this.clearLocalStorage();
    }

    public clearAllSessionsStorage() {

        this.clearInstanceSessionStorage();
        localStorage.removeItem(LocalStoreManager.DBKEY_SYNC_KEYS);

        localStorage.setItem('clearAllSessionsStorage', '_dummy');
        localStorage.removeItem('clearAllSessionsStorage');
    }

    public clearInstanceSessionStorage() {

        sessionStorage.clear();
        this.syncKeys = [];
    }

    public clearLocalStorage() {
        localStorage.clear();
    }

    public addToSessionStorage(data: any, key: string) {

        this.addToSessionStorageHelper(data, key);
        this.addToSyncKeysBackup(key);

        this.localStorageSetItem('addToSessionStorage', { key: key, data: data });
        localStorage.removeItem('addToSessionStorage');
    }

    public addToSessionStorageHelper(data: any, key: string) {

        this.addToSyncKeysHelper(key);
        this.sessionStorageSetItem(key, data);
    }

    public removeFromSessionStorage(keyToRemove: string) {

        this.removeFromSessionStorageHelper(keyToRemove);
        this.removeFromSyncKeysBackup(keyToRemove);

        localStorage.setItem('removeFromSessionStorage', keyToRemove);
        localStorage.removeItem('removeFromSessionStorage');
    }

    public removeFromSessionStorageHelper(keyToRemove: string) {

        sessionStorage.removeItem(keyToRemove);
        this.removeFromSyncKeysHelper(keyToRemove);
    }

    public testForInvalidKeys(key: string) {

        if (!key)
            throw new Error("key cannot be empty")

        if (this.reservedKeys.some(x => x == key))
            throw new Error(`The storage key "${key}" is reserved and cannot be used. Please use a different key`);
    }

    public syncKeysContains(key: string) {

        return this.syncKeys.some(x => x == key);
    }

    public loadSyncKeys() {

        if (this.syncKeys.length)
            return;

        this.syncKeys = this.getSyncKeysFromStorage();
    }

    public getSyncKeysFromStorage(defaultValue: string[] = []): string[] {

        let data = this.localStorageGetItem(LocalStoreManager.DBKEY_SYNC_KEYS);

        if (data == null)
            return defaultValue;
        else
            return <string[]>data;
    }

    public addToSyncKeys(key: string) {

        this.addToSyncKeysHelper(key);
        this.addToSyncKeysBackup(key);

        localStorage.setItem('addToSyncKeys', key);
        localStorage.removeItem('addToSyncKeys');
    }

    public addToSyncKeysBackup(key: string) {

        let storedSyncKeys = this.getSyncKeysFromStorage();

        if (!storedSyncKeys.some(x => x == key)) {
            storedSyncKeys.push(key);
            this.localStorageSetItem(LocalStoreManager.DBKEY_SYNC_KEYS, storedSyncKeys);
        }
    }

    public removeFromSyncKeysBackup(key: string) {

        let storedSyncKeys = this.getSyncKeysFromStorage();

        let index = storedSyncKeys.indexOf(key);

        if (index > -1) {
            storedSyncKeys.splice(index, 1);
            this.localStorageSetItem(LocalStoreManager.DBKEY_SYNC_KEYS, storedSyncKeys);
        }
    }

    public addToSyncKeysHelper(key: string) {

        if (!this.syncKeysContains(key))
            this.syncKeys.push(key);
    }

    public removeFromSyncKeys(key: string) {

        this.removeFromSyncKeysHelper(key);
        this.removeFromSyncKeysBackup(key);

        localStorage.setItem('removeFromSyncKeys', key);
        localStorage.removeItem('removeFromSyncKeys');
    }

    public removeFromSyncKeysHelper(key: string) {

        let index = this.syncKeys.indexOf(key);

        if (index > -1) {
            this.syncKeys.splice(index, 1);
        }
    }

    public saveSessionData(data: any, key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        this.removeFromSyncKeys(key);
        localStorage.removeItem(key);
        this.sessionStorageSetItem(key, data);
    }

    public saveSyncedSessionData(data: any, key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        localStorage.removeItem(key);
        this.addToSessionStorage(data, key);
    }

    public savePermanentData(data: any, key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        this.removeFromSessionStorage(key);
        this.localStorageSetItem(key, data);
    }

    public moveDataToSessionStorage(key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        let data = this.getData(key);

        if (data == null)
            return;

        this.saveSessionData(data, key);
    }

    public moveDataToSyncedSessionStorage(key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        let data = this.getData(key);

        if (data == null)
            return;

        this.saveSyncedSessionData(data, key);
    }

    public moveDataToPermanentStorage(key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        let data = this.getData(key);

        if (data == null)
            return;

        this.savePermanentData(data, key);
    }

    public exists(key = LocalStoreManager.DBKEY_USER_DATA) {

        let data = sessionStorage.getItem(key);

        if (data == null)
            data = localStorage.getItem(key);

        return data != null;
    }

    public getData(key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        let data = this.sessionStorageGetItem(key);

        if (data == null)
            data = this.localStorageGetItem(key);

        return data;
    }

    public getDataObject<T>(key = LocalStoreManager.DBKEY_USER_DATA, isDateType = false): T {

        let data = this.getData(key);

        if (data != null) {
            if (isDateType)
                data = new Date(data);

            return <T>data;
        }
        else {
            return null;
        }
    }

    public deleteData(key = LocalStoreManager.DBKEY_USER_DATA) {

        this.testForInvalidKeys(key);

        this.removeFromSessionStorage(key);
        localStorage.removeItem(key);
    }

    public localStorageSetItem(key: string, data: any) {
        localStorage.setItem(key, JSON.stringify(data));
    }

    public sessionStorageSetItem(key: string, data: any) {
        sessionStorage.setItem(key, JSON.stringify(data));
    }

    public localStorageGetItem(key: string) {
        return Utilities.JSonTryParse(localStorage.getItem(key));
    }

    public sessionStorageGetItem(key: string) {
        return Utilities.JSonTryParse(sessionStorage.getItem(key));
    }

    public onInit() {
        setTimeout(() => {
            this.initEvent.next();
            this.initEvent.complete();
        });
    }

    public getInitEvent(): Observable<{}> {
        return this.initEvent.asObservable();
    }
}