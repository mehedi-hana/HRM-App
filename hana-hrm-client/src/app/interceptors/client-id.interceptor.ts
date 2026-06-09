import { HttpInterceptorFn } from '@angular/common/http';

export const clientIdInterceptor: HttpInterceptorFn = (req, next) => {
  const modifiedReq = req.clone({
    setHeaders: {
      'X-Client-Id': '10001001',
    },
  });

  return next(modifiedReq);
};